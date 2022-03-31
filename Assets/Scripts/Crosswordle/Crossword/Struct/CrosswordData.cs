using System.Collections.Generic;
using UnityEngine;

public readonly struct WordData
{
    public readonly string Word;
    public readonly Vector2Int StartPosition;
    public readonly bool IsHorizontal;

    public WordData(string word, Vector2Int pos, bool horizontal)
    {
        Word = word;
        StartPosition = pos;
        IsHorizontal = horizontal;
    }

    public List<Vector2Int> MatchingLetters(WordData other)
    {
        var matches = new List<Vector2Int>();
        for (var i = 0; i < Word.Length; i++)
        {
            for (var j = 0; j < other.Word.Length; j++)
            {
                if (Word[i] == other.Word[j])
                {
                    matches.Add(new Vector2Int(i, j));
                }
            }
        }
        return matches;
    }


    private bool Adjacent(WordData other)
    {

    }
    private bool Intersect(WordData other)
    {

    }

    private Vector2Int Intersection(WordData other)
    {

    }

    // Placement is illegal if:
    // 1. The two words are adjacent and don't intersect.
    // 2. The two words are parallel and intersect.
    // 3. The two words are orthogonal, intersect, and have different characters where they intersect.
    private bool IsLegal(WordData other)
    {
        var intersects = Intersect(other);
        if (!intersects || IsHorizontal == other.IsHorizontal)
        {
            return Adjacent(other);
        }
        var intersection = Intersection(other);
        return Word[intersection.x] == other.Word[intersection.y];
    }

    public bool IsLegal(WordData[] words, int currentIndex)
    {
        for (var i = 0; i < currentIndex; i++)
        {
            if (!IsLegal(words[i]))
            {
                return false;
            }
        }
        return true;
    }
}

public readonly struct CrosswordData 
{
    public readonly WordData[] Words;

    public static CrosswordData GenerateCrossword(string[] words)
    {
        var crosswords = CrosswordUtils.GenerateCrossword(words);
        if (crosswords == null)
        {
            throw new System.Exception("Couldn't find valid crossword!");
        }
        return new CrosswordData(crosswords);
    }
    private CrosswordData(WordData[] crosswords)
    {
        Words = crosswords;
    }
}


public static class CrosswordUtils
{
    private static List<Vector2Int> MatchingLetters(string word1, string word2)
    {
        var matches = new List<Vector2Int>();
        for (var i = 0; i < word1.Length; i++)
        {
            for (var j = 0; j < word2.Length; j++)
            {
                if (word1[i] == word2[j])
                {
                    matches.Add(new Vector2Int(i, j));
                }
            }
        }
        return matches;
    }

    private static bool IsLegal(string word1, Vector2Int pos1, isHorz)

    private static bool IsLegalPlacement(string[] words, Vector2Int[] startPositions, bool[] isHorizonal, int currentIndex)
    {
        for (var i = 0; i < words.Length; i++)
        {

        }
    } 

    private static bool GenerateCrossword(WordData[] words, int currentIndex)
    {
        // Base case: we're out of words to add
        if (currentIndex == words.Length)
            return true;


        // Inductive case: try every position and see if it's a valid placement. If so, increment the index and go one level deeper.
        for (var i = 0; i < currentIndex;i++)
        {
            // Find all of the matching letters.
            var matches = words[currentIndex].MatchingLetters(words[i]);

            for (var j = 0; j < matches.Count; j++)
            {
                var match = matches[j];
            }

        }

        // If we run out of things to try, notify the caller.
        return false;
    }

    public static WordData[] GenerateCrossword(string[] words)
    {
        // Words will be placed relevative to the first one, so the position and orientation can be set arbitrarily.
        var crosswords = new WordData[words.Length];
        crosswords[0] = new WordData(words[0], Vector2Int.zero, true);
        var success = GenerateCrossword(crosswords, 1);
        if (!success)
            return null;
        return crosswords;
    }

}