using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordDictionary 
{
    private HashSet<Word> _dictionary;
    private List<List<Word>> _dictionaries;

    public static WordDictionary GenerateDictionary()
    {
        var dict =  new WordDictionary();
        dict.InitializeDictionary();
        return dict;
    }

    private void InitializeDictionary()
    {
        _dictionary = new HashSet<Word>();
        _dictionaries = new List<List<Word>>();
        var words = File.ReadLines("Assets/Data/words.txt");
        foreach (var word in words)
        {
            var guess = new Word(word);
            _dictionary.Add(guess);
            while (_dictionaries.Count < word.Length)
            {
                _dictionaries.Add(new List<Word>());
            }
            _dictionaries[word.Length - 1].Add(guess);
        }
    }

    public bool IsValidWord(Word guess)
    {
        return _dictionary.Contains(guess);
    }


    public Word GetRandomWord(int length)
    {
        return _dictionaries[length - 1][Random.Range(0, _dictionaries[length - 1].Count)];
    }
}
