using System.Collections;
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
        foreach (var w in _words)
        {
            sb.AppendLine();
            sb.Append(w.ToString());
        }
        text.text = sb.ToString();
    }


    public void AddWord(Word word)
    {
        _words.Add(word);
        UpdateText();
    }
}
