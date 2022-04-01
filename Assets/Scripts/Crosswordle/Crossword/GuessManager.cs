using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int WordLength;
    [SerializeField] private GuessRenderer Renderer;
    [SerializeField] private string TestGuess;

    private Word _guess;

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
    public void SubmitWord()
    {
        _guess = new Word();
    }

    public void OnValidate()
    {
        Guess = new Word(TestGuess);
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
