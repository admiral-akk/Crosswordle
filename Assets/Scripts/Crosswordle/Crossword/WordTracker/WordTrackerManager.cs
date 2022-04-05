using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class WordTrackerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int MaxCount;
    private List<Word> _words;
    // Start is called before the first frame update
    void Awake()
    {
        ResetGame();
    }

    private void UpdateText()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _words.Count; i++)
        {
            sb.AppendLine();
            sb.Append(string.Format("{0}: {1}", i+1, _words[i]));
        }
        for (var i = _words.Count; i < MaxCount; i++)
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

    public bool GameOver => _words.Count >= MaxCount;

    public void ResetGame()
    {

        _words = new List<Word>();
        UpdateText();
    }
}
