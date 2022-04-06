using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CrosswordDictionary : IManagedObject { 

    private List<Word> _answers;
    private HashSet<Word> _dictionary;

    private static string _wordPath = Application.streamingAssetsPath+ "/LegalWords.txt";
    private static string _answerPath = Application.streamingAssetsPath+ "/LegalAnswers.txt";

    public IEnumerator Initialize()
    {
       yield return  FillWordCollection(_wordPath, _dictionary);
        yield return FillWordCollection(_answerPath, _answers);
    }

            public CrosswordDictionary()
    {
        _dictionary = new HashSet<Word>();
        _answers = new List<Word>();
    }

    private static IEnumerator FillWordCollection(string filePath, ICollection<Word> collection)
    {
        if (filePath.Contains("http:"))
        {
           UnityWebRequest www = UnityWebRequest.Get(filePath);
           yield return www.SendWebRequest();
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
