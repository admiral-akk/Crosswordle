using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WordDictionary { 

    private List<Word> _answers;
    private HashSet<Word> _dictionary;

    private static string _wordPath = Application.streamingAssetsPath+ "/LegalWords.txt";
    private static string _answerPath = Application.streamingAssetsPath+ "/LegalAnswers.txt";

    public static WordDictionary GenerateDictionary()
    {
        var dict =  new WordDictionary();

        FillWordCollection(_wordPath, dict._dictionary);
        FillWordCollection(_answerPath, dict._answers);
        return dict;
    }


    private WordDictionary()
    {
        _dictionary = new HashSet<Word>();
        _answers = new List<Word>();
    }

    private static void FillWordCollection(string filePath, ICollection<Word> collection)
    {
        Debug.Log(filePath);
        if (filePath.Contains("http:"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
             www.SendWebRequest();
                while (!www.isDone) { }

                foreach (var word in www.downloadHandler.text.Split('\n'))
                {
                    if (word.Length != 5)
                        continue;
                    collection.Add(new Word(word));
                }
        }
        else
        {
            foreach (var word in File.ReadLines(filePath))
            {
                if (word.Length != 5)
                    continue;
                collection.Add(new Word(word));
            }
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
