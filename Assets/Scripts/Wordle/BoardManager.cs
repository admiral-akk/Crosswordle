using Assets.Scripts.Structs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject LetterSquarePrefab;
    [SerializeField, Range(3, 7)] private int WordLength;
    [SerializeField, Range(1, 10)] private int GuessLimit;
    [SerializeField, Range(0, 1)] private float WordSpacing;
    [SerializeField, Range(0, 1)] private float GuessSpacing;
    [SerializeField] private float YOffset;

    [SerializeField] private KeyboardManager keyboard;
    [SerializeField] private AnswerManager answer;
    [SerializeField] private GameManager game;

    private List<LetterSquare> _letterSquares;
    private WordDictionary _words;
    private bool _resetBoard;
    private int _currentWord;
    private int _currentLetter;

    private void Awake()
    {
        _letterSquares = new List<LetterSquare>();
        _words =  WordDictionary.GenerateDictionary();
    }

    private void OnValidate()
    {
        _resetBoard = true;
    }

    private void ResetBoard()
    {
        foreach (var letterSquare in _letterSquares)
            Destroy(letterSquare.gameObject);

        _letterSquares.Clear();
        InitializeBoard();
    }

    private void Update()
    {
        if (!_resetBoard)
            return;
        _resetBoard = false;
        ResetBoard();
    }

    private Vector3 Spacing => new Vector3(0.8f + WordSpacing, 0.8f + GuessSpacing);
    private Vector3 Offset => new Vector3((1f - WordLength) / 2, (GuessLimit - 1f) / 2 + YOffset);

    private Vector3 Position(int letterIndex, int guessIndex)
    {
        return Vector3.Scale(Spacing, new Vector3(letterIndex, -guessIndex) + Offset);
    }

    private Guess _targetWord;

    private void InitializeBoard()
    {
        _currentWord = 0;
        _currentLetter = 0;
        _targetWord = new Guess((string)_words.GetRandomWord());
        for (var guess = 0; guess < GuessLimit; guess++)
        {
            for (var word = 0; word < WordLength; word++)
            {
                var letterSquare = Instantiate(LetterSquarePrefab, transform).GetComponent<LetterSquare>();
                letterSquare.transform.localPosition = Position(word, guess);
                _letterSquares.Add(letterSquare);
            }
        }
    }

    private List<LetterSquare> CurrentSquares
    {
        get
        {
            return _letterSquares.GetRange(((CurrentIndex - 1) / WordLength) * WordLength, WordLength);
        }

    }

    private Guess CurrentGuess
    {
        get
        {
            return (Guess)CurrentSquares.Select(l => l.Letter).Aggregate("", (c, s) => c + s);
        }
    }

    private int CurrentIndex => _currentLetter + _currentWord * WordLength;

    public void NewGame()
    {
        ResetBoard();
    }

    private void ShakeRow()
    {
        foreach (var square in CurrentSquares)
        {
            if (square.GetComponent<Shake>() != null)
                continue;
            square.gameObject.AddComponent(typeof(Shake));
        }
    }

    public void SubmitWord()
    {
        if (_currentLetter < WordLength || !_words.IsValidWord(new Word((string)CurrentGuess)))
        {
            ShakeRow();
            return;
        }

        var result = new GuessResult(CurrentGuess, _targetWord);
        for (var i = 0; i < result.Results.Length; i++)
        { 
            CurrentSquares[i].HandleResult(result.Results[i], i * 0.25f);
        }

        _currentLetter = 0;
        _currentWord++;
        keyboard.HandleResult(result);
        if (result.CorrectAnswer())
        {
            answer.Success(_targetWord);
            game.GameOver();
        }
        else if (_currentWord == GuessLimit)
        {
            answer.GameOver(_targetWord);
            game.GameOver();
        }
    }

    public void SubmitLetter(char c)
    {
        if (_currentLetter == WordLength)
            return;
        if (_currentWord == GuessLimit)
            return;
        _letterSquares[CurrentIndex].Letter = c;
        _currentLetter++;
    }

    public void DeleteLetter()
    {
        if (_currentLetter == 0)
            return;
        _letterSquares[CurrentIndex - 1].ClearLetter();
        _currentLetter--;
    }
}
