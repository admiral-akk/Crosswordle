using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int WordLength;
    [SerializeField] private GuessRenderer Renderer;

    private Word _guess;
    private WordDictionary _dictionary;
    private CharacterKnowledge _knowledge;
    private List<Word> _guesses;

    private CharacterKnowledge[] PerLetterKnowledge {

        get {
            var arr = new CharacterKnowledge[WordLength];
            for (var i = 0; i < WordLength; i++)
            {
                arr[i] = new CharacterKnowledge(_knowledge);
                foreach (var word in _guesses)
                {
                    arr[i].SetWrong(word[i]);
                }
            }
            return arr;
            }
        }

    private Word Guess
    {
        get
        {
            return _guess;
        }
        set
        {
            _guess = value;
            Renderer.Render(_guess, PerLetterKnowledge);
        }
    }

    private void Awake()
    {
        _dictionary = WordDictionary.GenerateDictionary();
        _guess = new Word("");
        _guesses = new List<Word>();
    }

    public void Register(CharacterKnowledge knowledge)
    {
        _knowledge = knowledge;
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
