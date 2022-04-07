using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class WordTrackerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private List<Word> _words;
    private int _guessCount;

    private void UpdateText()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _words.Count; i++)
        {
            sb.AppendLine();
            sb.Append(string.Format("{0}: {1}", i+1, _words[i]));
        }
        for (var i = _words.Count; i < _guessCount; i++)
        {
            sb.AppendLine();
            sb.Append(string.Format("{0}: ", i + 1));
        }
        text.text = sb.ToString();
    }

    public void AddWord(Word word)
    {
        _words.Add(word);
        UpdateText();
    }

    public bool PlayerLost => _words.Count >= _guessCount;

    public void ResetGame(int guessCount)
    {
        _guessCount = guessCount;
        _words = new List<Word>();
        UpdateText();
    }
}
