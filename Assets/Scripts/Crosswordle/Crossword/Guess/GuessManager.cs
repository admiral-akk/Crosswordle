using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int WordLength;
    [SerializeField] private GuessRenderer Renderer;

    private Word _guess;
    private GuessKnowledge _knowledge;
    private List<Word> _guesses;
    private DictionaryManager _dictionary;

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
        ResetGame();
    }

    public void ResetGame()
    {
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
        if (Guess.Length < WordLength)
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
        if (Guess.Length < WordLength)
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
