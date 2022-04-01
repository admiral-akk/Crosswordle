using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordDictionary { 

    private List<Word> _answers;
    private HashSet<Word> _dictionary;

    public static WordDictionary GenerateDictionary()
    {
        var dict =  new WordDictionary();
        FillWordCollection("Assets/Data/LegalWords.txt", dict._dictionary);
        FillWordCollection("Assets/Data/LegalAnswers.txt", dict._answers);
        return dict;
    }


    private WordDictionary()
    {
        _dictionary = new HashSet<Word>();
        _answers = new List<Word>();
    }

    private static void FillWordCollection(string filePath, ICollection<Word> collection)
    {
        var words = File.ReadLines(filePath);
        foreach (var word in words)
        {
            collection.Add(new Word(word));
        }
    }

    public bool IsValidWord(Word guess)
    {
        return _dictionary.Contains(guess);
    }


    public Word GetRandomWord()
    {
        return _answers[Random.Range(0, _answers.Count)];
    }
}
