using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Manager<BoardManager>
{
    [SerializeField] private GameObject LetterSquarePrefab;
    [SerializeField, Range(3,7)] private int WordLength;
    [SerializeField, Range(1,10)] private int GuessLimit;
    [SerializeField, Range(0, 1)] private float WordSpacing;
    [SerializeField, Range(0, 1)] private float GuessSpacing;

    private List<LetterSquare> _letterSquares;
    private bool _resetBoard;
    private int _currentWord;
    private int _currentLetter;

    protected override void ManagerAwake()
    {
        _letterSquares = new List<LetterSquare>();
        ResetBoard();
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

    private Vector3 Spacing =>new Vector3(1 + WordSpacing, 1 + GuessSpacing);
    private Vector3 Offset => new Vector3((1f - WordLength) / 2, (GuessLimit - 1f) / 2);

    private Vector3 Position(int letterIndex, int guessIndex)
    {
        return Vector3.Scale(Spacing, new Vector3(letterIndex, -guessIndex) + Offset);
    }

    private void InitializeBoard()
    {
        _currentWord = 0;
        _currentLetter = 0;
        for (var guess = 0; guess < GuessLimit; guess++)
        {
            for (var word = 0; word < WordLength; word++)
            {
                var letterSquare = Instantiate(LetterSquarePrefab, transform);
                letterSquare.transform.localPosition = Position(word, guess);
                _letterSquares.Add(letterSquare.GetComponent<LetterSquare>());
            }
        }
    }

    public static void SubmitWord()
    {
        if (Instance._currentLetter < Instance.WordLength)
            return;
        Instance._currentLetter = 0;
        Instance._currentWord++;
    }

    public static void SubmitLetter(char c)
    {
        if (Instance._currentLetter == Instance.WordLength)
            return;
        Instance._letterSquares[CurrentIndex].SetLetter(c);
        Instance._currentLetter++;
    }

    public static int CurrentIndex => Instance._currentLetter + Instance._currentWord * Instance.WordLength;

    public static void DeleteLetter()
    {
        if (Instance._currentLetter == 0)
            return;
        Instance._letterSquares[CurrentIndex-1].ClearLetter();
        Instance._currentLetter--;
    }
}
