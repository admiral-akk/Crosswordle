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

    [SerializeField] private WordManager words;

    private List<LetterSquare> _letterSquares;
    private bool _resetBoard;
    private int _currentWord;
    private int _currentLetter;

 private void Awake()
    {
        _letterSquares = new List<LetterSquare>();
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

    private Vector3 Spacing => new Vector3(1 + WordSpacing, 1 + GuessSpacing);
    private Vector3 Offset => new Vector3((1f - WordLength) / 2, (GuessLimit - 1f) / 2);

    private Vector3 Position(int letterIndex, int guessIndex)
    {
        return Vector3.Scale(Spacing, new Vector3(letterIndex, -guessIndex) + Offset);
    }

    private Guess _targetWord;

    private void InitializeBoard()
    {
        _currentWord = 0;
        _currentLetter = 0;
        _targetWord = words.GetRandomWord(WordLength);
        Debug.Log("Target word is: '" + _targetWord + "'");
        for (var guess = 0; guess < GuessLimit; guess++)
        {
            for (var word = 0; word < WordLength; word++)
            {
                var letterSquare = Instantiate(LetterSquarePrefab, transform).GetComponent<LetterSquare>();
                letterSquare.transform.localPosition = Position(word, guess);
                letterSquare.SetState(LetterSquare.State.None);
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

    private Guess CurrentGuess {
        get
        {
           return (Guess)CurrentSquares.Select(l => l.Letter).Aggregate("", (c, s) =>  c+s);
        }
    }

    private int CurrentIndex => _currentLetter + _currentWord * WordLength;
    public  void SubmitWord()
    {
        Debug.Log("Current guess: '" + CurrentGuess + "'");
        Debug.Log("Target word: '" + _targetWord + "'");
        if (_currentLetter < WordLength)
            return;
        if (!words.IsWord(CurrentGuess))
            return;
        Debug.Log("Current guess: '" + CurrentGuess + "'");
        Debug.Log("Target word: '" + _targetWord + "'");

        for (var i = 0; i < CurrentSquares.Count; i++)
        {
            var square = CurrentSquares[i];
            if (_targetWord[i] == CurrentGuess[i])
            {
                square.SetState(LetterSquare.State.RightPosition);
            }
            else if (_targetWord.Any(l => l == CurrentGuess[i]))
            {
                square.SetState(LetterSquare.State.WrongPosition);
            }
            else
            {
                square.SetState(LetterSquare.State.Wrong);
            }
        }

        _currentLetter = 0;
        _currentWord++;
    }

    public  void SubmitLetter(char c)
    {
        if (_currentLetter == WordLength)
            return;
        _letterSquares[CurrentIndex].Letter = c;
        _currentLetter++;
    }

    public  void DeleteLetter()
    {
        if (_currentLetter == 0)
            return;
        _letterSquares[CurrentIndex-1].ClearLetter();
        _currentLetter--;
    }
}
