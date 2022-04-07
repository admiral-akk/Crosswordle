using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField] private GuessRenderer Renderer;

    private Word _guess;
    private GuessKnowledge _knowledge;
    private List<Word> _guesses;
    private DictionaryManager _dictionary;
    private int _wordLength;

    private Word Guess
    {
        get
        {
            return _guess;
        }
        set
        {
            _guess = value;
            Renderer.Render(_guess, _knowledge, _wordLength);
        }
    }

    public void ResetGame(int wordLength)
    {
        _wordLength = wordLength;
        Guess = new Word("");
        _guesses = new List<Word>();
    }

    public void UpdateGuessKnowledge(GuessKnowledge knowledge)
    {
        _knowledge = knowledge;
        _knowledge.AddGuesses(_guesses);
    }

    public Word? SubmitWord()
    {
        if (Guess.Length < _wordLength)
            return null;
        if (!_dictionary.IsValidWord(Guess))
            return null;
        var ret = Guess;
        _guesses.Add(Guess);
        Guess = new Word("");
        return ret;
    }

    public void SubmitLetter(char c)
    {
        if (Guess.Length < _wordLength)
            Guess += c;
    }

    public void DeleteLetter()
    {
        if (Guess.Length > 0)
            Guess = Guess.RemoveEnd();
    }
    public void Initialize(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
    }

    public void GameOver()
    {
        Renderer.gameObject.SetActive(false);
    }
}
