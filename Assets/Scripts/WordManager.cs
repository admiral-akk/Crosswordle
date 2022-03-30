using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    private HashSet<string> _dictionary;
    private List<List<string>> _dictionaries;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _dictionary = new HashSet<string>();
        _dictionaries = new List<List<string>>();
        var words = File.ReadLines("Assets/Data/words.txt");
        foreach (var word in words)
        {
            _dictionary.Add(word.ToUpper());
            while (_dictionaries.Count < word.Length)
            {
                _dictionaries.Add(new List<string>());
            }
            _dictionaries[word.Length-1].Add(word);
        }
    }

    public bool IsWord(string word)
    {
        return _dictionary.Contains(word);
    }


    public string GetRandomWord(int length)
    {
        return _dictionaries[length-1][Random.Range(0, _dictionaries[length-1].Count)];
    }
}
