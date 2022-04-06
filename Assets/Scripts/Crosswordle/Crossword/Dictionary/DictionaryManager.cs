using System.Collections;
using UnityEngine;
using System;

public class DictionaryManager : MonoBehaviour, IManager 
{
    private bool _isLoaded;
    private CrosswordDictionary _data;

    private IEnumerator InitializeCoroutine(Action callback)
    {
        _isLoaded = false;
        _data = new CrosswordDictionary();
        yield return _data.Initialize();
        _isLoaded = true;
        callback();
    }

    public bool Ready()
    {
        return _isLoaded;
    }

    public bool IsValidWord(Word guess)
    {
        return _data.IsValidWord(guess);
    }

    public Word GetRandomWord()
    {
        return _data.GetRandomWord();
    }

    public void Initialize(Action callback)
    {
        StartCoroutine(InitializeCoroutine(callback));
    }
}
