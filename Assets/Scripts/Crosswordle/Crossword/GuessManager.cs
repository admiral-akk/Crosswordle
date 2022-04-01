using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int WordLength;

    private Word _guess;
    public void SubmitWord()
    {
        _guess = new Word();
    }

    public void SubmitLetter(char c)
    {
        if (_guess.Length < WordLength)
            _guess += c;
    }

    public void DeleteLetter()
    {
        if (_guess.Length > 0)
            _guess = _guess.RemoveEnd();
    }
}
