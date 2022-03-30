using Assets.Scripts.Structs;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    private HashSet<Guess> _dictionary;
    private List<List<Guess>> _dictionaries;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _dictionary = new HashSet<Guess>();
        _dictionaries = new List<List<Guess>>();
        var words = File.ReadLines("Assets/Data/words.txt");
        foreach (var word in words)
        {
            var guess = new Guess(word);
            _dictionary.Add(guess);
            while (_dictionaries.Count < word.Length)
            {
                _dictionaries.Add(new List<Guess>());
            }
            _dictionaries[word.Length-1].Add(guess);
        }
    }

    public bool IsWord(Guess guess)
    {
        return _dictionary.Contains(guess);
    }


    public Guess GetRandomWord(int length)
    {
        return _dictionaries[length-1][Random.Range(0, _dictionaries[length-1].Count)];
    }
}
