using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class WordTrackerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private List<Word> _words;
    // Start is called before the first frame update
    void Awake()
    {
        _words = new List<Word>();
        UpdateText();
    }

    private void UpdateText()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _words.Count; i++)
        {
            sb.AppendLine();
            sb.Append(string.Format("{0}: {1}", i+1, _words[i]));
        }
        text.text = sb.ToString();
    }

    public void AddWord(Word word)
    {
        _words.Add(word);
        UpdateText();
    }
}
