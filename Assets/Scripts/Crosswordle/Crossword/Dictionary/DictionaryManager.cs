using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour, IManager 
{
    private bool _isLoaded;
    private CrosswordDictionary _data;

    private void Awake()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        _isLoaded = false;
        _data = new CrosswordDictionary();
        yield return _data.Initialize();
        _isLoaded = true;
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

    public void ResetManager()
    {
        StartCoroutine(Initialize());
    }
}
