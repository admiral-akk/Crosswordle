using System.Collections.Generic;
using UnityEngine;

public class CrosswordGenerator : MonoBehaviour
{
    [SerializeField] private string[] Words;

    private WordDictionary _dictionary;

    private void OnValidate()
    {
        if (_dictionary == null)
        {
            _dictionary = WordDictionary.GenerateDictionary();
        }

        var words = new HashSet<Word>();
    }
}
