using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CrosswordDictionary 
{
    private List<List<Word>> _validAnswers;
    private List<HashSet<Word>> _validWords;

    private static string _wordPath = Application.streamingAssetsPath + "/LegalWords.txt";
    private static string _answerPath = Application.streamingAssetsPath + "/LegalAnswers.txt";

    public IEnumerator Initialize()
    {
        var tempWords = new List<List<Word>>();
        yield return FillWordCollection(_wordPath, tempWords);
        InitializeValidWords(tempWords);
        yield return FillWordCollection(_answerPath, _validAnswers);
    }

    private void InitializeValidWords(List<List<Word>> words)
    {
        _validWords = new List<HashSet<Word>>(words.Count);
        for (var i = 0; i < words.Count; i++)
        {
            _validWords[i] = new HashSet<Word>(words[i]);
        }
    }

    public CrosswordDictionary()
    {
        _validWords = new List<HashSet<Word>>();
        _validAnswers = new List<List<Word>>();
    }

    private static void AddWord(List<List<Word>> collection, string possibleWord)
    {
        var wordLength = possibleWord.Length;
        if (wordLength == 0)
            return;
        while (collection.Count < wordLength)
            collection.Add(new List<Word>());
        collection[wordLength - 1].Add(possibleWord);
    }

    private static IEnumerator FillWordCollectionViaWeb(string url, List<List<Word>> collection)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        foreach (var word in www.downloadHandler.text.Split('\n'))
        {
            AddWord(collection, word);
        }
    }
    private static void FillWordCollectionViaFile(string filePath, List<List<Word>> collection)
    {
        foreach (var word in File.ReadLines(filePath))
        {
            AddWord(collection, word);
        }
    }

    private static IEnumerator FillWordCollection(string path, List<List<Word>> collection)
    {
        if (path.Contains("http:"))
        {
            yield return FillWordCollectionViaWeb(path, collection);
        }
        else
        {
            FillWordCollectionViaFile(path, collection);
        }
    }

    public bool IsValidWord(Word guess)
    {
        var len = guess.Length;
        if (len == 0)
            return false;
        if (len >= _validWords.Count)
            return false;
        return _validWords[len-1].Contains(guess);
    }


    public Word GetRandomWord(int length = 5)
    {
        if (length < 1 || length >= _validAnswers.Count || _validAnswers[length - 1].Count == 0)
            throw new System.Exception(string.Format("Length {0} out of array bounds!",length));
        var words = _validAnswers[length - 1];
        if (words.Count == 0)
            throw new System.Exception(string.Format("No words at length {0}!", length));
        return words[Random.Range(0, words.Count)];
    }
}
