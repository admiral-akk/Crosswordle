using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : Manager<WordManager>
{
    private HashSet<string> _dictionary;
    private List<List<string>> _dictionaries;

    protected override void ManagerAwake()
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
            _dictionary.Add(word.ToLower());
            while (_dictionaries.Count < word.Length)
            {
                _dictionaries.Add(new List<string>());
            }
            _dictionaries[word.Length-1].Add(word);
        }
    }

    private bool IsWordInternal(string word)
    {
        return _dictionary.Contains(word);
    }

    public static bool IsWord(string word)
    {
        return Instance.IsWordInternal(word.ToLower());
    }

    private string GetRandomWordInternal(int length)
    {
        return _dictionaries[length-1][Random.Range(0, _dictionaries[length-1].Count)];
    }

    public static string GetRandomWord(int length)
    {
        return Instance.GetRandomWordInternal(length);
    }
}
