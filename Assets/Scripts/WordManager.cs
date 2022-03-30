using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : Manager<WordManager>
{
    private HashSet<string> _dictionary;

    protected override void ManagerAwake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _dictionary = new HashSet<string>();
        var words = File.ReadLines("Assets/Data/words.txt");
        foreach (var word in words)
        {
            _dictionary.Add(word.ToLower());
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
}
