using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int WordLength;
    [SerializeField] private GuessRenderer Renderer;

    private Word _guess;
    private WordDictionary _dictionary;
    private GuessKnowledge _knowledge;
    private List<Word> _guesses;


    private Word Guess
    {
        get
        {
            return _guess;
        }
        set
        {
            _guess = value;
            Renderer.Render(_guess, _knowledge);
        }
    }

    private void Awake()
    {
        _dictionary = WordDictionary.GenerateDictionary();
        _guess = new Word("");
        _guesses = new List<Word>();
    }

    public void UpdateGuessKnowledge(GuessKnowledge knowledge)
    {
        _knowledge = knowledge;
        _knowledge.AddGuesses(_guesses);
    }

    public Word? SubmitWord()
    {
        if (_guess.Length < WordLength)
            return null;
        if (!_dictionary.IsValidWord(_guess))
            return null;
        var ret = _guess;
        _guesses.Add(_guess);
        Guess = new Word("");
        return ret;
    }

    public void SubmitLetter(char c)
    {
        if (Guess.Length < WordLength)
            Guess += c;
    }

    public void DeleteLetter()
    {
        if (Guess.Length > 0)
            Guess = Guess.RemoveEnd();
    }
}
