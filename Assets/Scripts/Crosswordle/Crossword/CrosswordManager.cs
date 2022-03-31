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

    private WordDictionary _dictionary;
    private CrosswordData _crossword;

    private void GenerateCrossword()
    {
        CrosswordData? bestCrossword = null;
        var words = new HashSet<Word>();
        var limit = SampleLimit;
        while (limit-- > 0)
        {
            words.Clear();
            while (words.Count < WordCount)
            {
                words.Add(_dictionary.GetRandomWord(5));
            }
            var crossword = CrosswordData.GenerateCrossword(words.Select(w => (string)w).ToArray());
            if (bestCrossword == null)
            {
                bestCrossword = crossword;
            }
            else if (crossword != null)
            {
                var (xDim, yDim) = (bestCrossword.Value.xDim, bestCrossword.Value.yDim);
                var (xDimNew, yDimNew) = (crossword.Value.xDim, crossword.Value.yDim);
                if (Mathf.Max(xDimNew,yDimNew) < Mathf.Max(xDim, yDim))
                {
                    bestCrossword = crossword;
                } else if (Mathf.Max(xDimNew, yDimNew) == Mathf.Max(xDim, yDim))
                {
                    if (xDimNew < xDim && yDimNew <= yDim)
                    {
                        bestCrossword = crossword;
                    }
                    if (xDimNew <= xDim && yDimNew < yDim)
                    {
                        bestCrossword = crossword;
                    }
                } 
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
    }


}
