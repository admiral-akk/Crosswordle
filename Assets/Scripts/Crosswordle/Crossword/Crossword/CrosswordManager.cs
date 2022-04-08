using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrosswordManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(1, 1000)] private int SampleLimit;
    [Header("Components")]
    [SerializeField] private CrosswordRenderer Renderer;

    private DictionaryManager _dictionary;
    private CrosswordKnowledge _knowledge;
    private CharacterKnowledge _globalKnowledge;
    private int _wordLength;
    private void GenerateCrosswordInternal(int wordCount, int wordLength)
    {
        CrosswordData bestCrossword = null;
        var words = new HashSet<Word>();
        var limit = SampleLimit;
        var compare = new CrosswordComparer();
        while (limit-- > 0)
        {
            words.Clear();
            while (words.Count < wordCount)
            {
                words.Add(_dictionary.GetRandomWord(wordLength));
            }

            var crossword = CrosswordData.GenerateCrossword(words.ToArray());

            if (crossword == null)
                continue;
            if (bestCrossword == null)
            {
                bestCrossword = crossword;
                continue;
            }
            if (compare.Compare(bestCrossword, crossword) > 0)
            {
                bestCrossword = crossword;
                continue;
            }
        }
        if (bestCrossword == null)
            throw new System.Exception("No crossword generated");
        _knowledge = new CrosswordKnowledge(bestCrossword);
        _globalKnowledge = new CharacterKnowledge();
    }

    public bool PlayerWon { 
        get
        {
            foreach (var problem in _knowledge.Problems)
            {
                for (var i = 0; i < problem.Length; i++)
                {
                    if (!problem.GetKnowledge(i).IsSolved)
                        return false;
                }
            }
            return true;
        }
    }

    public void GenerateCrossword(int wordCount, int wordLength)
    {
        _wordLength = wordLength;
        GenerateCrosswordInternal(wordCount, wordLength);
        Renderer.Render(_knowledge);
    }

    public void HandleGuess(Word word)
    {
        _knowledge.Guess(word);
        UpdateGlobalKnowledge(word);
        Renderer.Render(_knowledge);
    }

    private void UpdateGlobalKnowledge(Word word)
    {
        _globalKnowledge.Guess(word);
        foreach (var problem in _knowledge.Problems)
        {
            for (var i = 0; i < problem.Length; i++)
            {
                _globalKnowledge.Update(problem.GetKnowledge(i));
            }
        }
    } 

    public CharacterKnowledge GetGlobalLetterKnowledge()
    {
        return _globalKnowledge;
    }

    public GuessKnowledge GenerateGuessKnowledge()
    {
        var guessKnowledge = new GuessKnowledge(_wordLength);
        foreach (var problem in _knowledge.Problems)
        {
            for (var i = 0; i < problem.Length; i++)
            {
                var k = problem.GetKnowledge(i);
                if (!k.IsSolved)
                {
                    guessKnowledge.AddHints(k.Hints, i);
                } else
                {
                    var c = k.Answer.Value;
                    guessKnowledge.AddCorrect(c, i);
                }
            }
        }
        return guessKnowledge;
    }

    public void Initialize(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
    }

    public void SpoilCrossword()
    {
        _knowledge.Spoil();
        Renderer.Render(_knowledge);
    }
}
