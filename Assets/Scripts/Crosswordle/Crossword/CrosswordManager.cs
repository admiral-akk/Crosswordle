using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrosswordManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(1, 20)] private int WordCount;
    [SerializeField, Range(1, 100)] private int SampleLimit;
    [Header("Components")]
    [SerializeField] private GameObject SquarePrefab;
    [SerializeField] private CrosswordRenderer Renderer;

    private WordDictionary _dictionary;
    private CrosswordData _crossword;

    private void GenerateCrossword()
    {
        CrosswordData? bestCrossword = null;
        var words = new HashSet<Word>();
        var limit = SampleLimit;
        var compare = new CrosswordComparer();
        while (limit-- > 0)
        {
            words.Clear();
            while (words.Count < WordCount)
            {
                words.Add(_dictionary.GetRandomWord(5));
            }
            
            var crossword = CrosswordData.GenerateCrossword(words.Select(w => (string)w).ToArray());

            if (crossword == null)
                continue;
            if (bestCrossword == null)
            {
                bestCrossword = crossword;
                continue;
            }
            if (compare.Compare(bestCrossword.Value, crossword.Value) > 0)
            {
                bestCrossword = crossword;
                continue;
            }
        }
        if (bestCrossword == null)
            throw new System.Exception("No crossword generated");
        _crossword = bestCrossword.Value;
    }

    private void OnValidate()
    {
        if (_dictionary == null)
        {
            _dictionary = WordDictionary.GenerateDictionary();
        }

        GenerateCrossword();
        Debug.Log(_crossword.ToString());
        Renderer.Render(_crossword);
    }


}
