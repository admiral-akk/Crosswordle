using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int WordLength;
    [SerializeField] private GuessRenderer Renderer;

    private Word _guess;
    private WordDictionary _dictionary;
    private Word Guess
    {
        get
        {
            return _guess;
        }
        set
        {
            _guess = value;
            Renderer.Render(_guess);
        }
    }

    private void Awake()
    {
        _dictionary = WordDictionary.GenerateDictionary();
        Guess = new Word("");
    }
    public Word? SubmitWord()
    {
        if (_guess.Length < WordLength)
            return null;
        if (!_dictionary.IsValidWord(_guess))
            return null;
        var ret = _guess;
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
