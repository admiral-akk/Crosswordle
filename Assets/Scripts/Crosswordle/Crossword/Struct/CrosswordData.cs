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
    public WordData(string word)
    {
        Word = word;
        StartPosition = Vector2Int.zero;
        IsHorizontal = true;
    }

    public Vector2Int GetOffset(Vector2Int match)
    {
        if (IsHorizontal)
        {
            return new Vector2Int(match.y + StartPosition.x, match.x + StartPosition.y);
        } else
        {
            return new Vector2Int(match.x + StartPosition.x, match.y + StartPosition.y);
        }
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

    private bool Adjacent(Vector2Int point)
    {
        var xDelta = point.x - StartPosition.x;
        var yDelta = point.y - StartPosition.y;
        var isAdjacent = yDelta > -2 && xDelta > -2;

        if (IsHorizontal)
        {
            isAdjacent &= yDelta < 2 && xDelta < Word.Length + 1;
        } else
        {
            isAdjacent &= xDelta < 2 && yDelta < Word.Length + 1;
        }
        return isAdjacent;
    }

    private bool Adjacent(WordData other)
    {
        if (!IsHorizontal)
            return other.Adjacent(this);
        var intersectionPoint = Intersection(other);
        return false;
    }
    private bool Intersect(WordData other)
    {
        if (!IsHorizontal)
            return other.Intersect(this);
        var intersectionPoint = Intersection(other);
        var xDelta = intersectionPoint.x - StartPosition.x;
        var yDelta = intersectionPoint.y - other.StartPosition.y;
        if (xDelta < 0)
            return false;
        if (yDelta < 0)
            return false;
        if (xDelta >= Word.Length)
            return false;
        if (yDelta >= other.Word.Length)
            return false;
        return true;
    }

    private Vector2Int Intersection(WordData other)
    {
        if (!IsHorizontal)
        {
            var reversedIntersection = other.Intersection(this);
            return new Vector2Int(reversedIntersection.y, reversedIntersection.x);
        }

        return new Vector2Int(other.StartPosition.x, other.StartPosition.y);
    }

    // Placement is illegal if:
    // 1. The two words are adjacent and don't intersect.
    // 2. The two words are parallel and intersect.
    // 3. The two words are orthogonal, intersect, and have different characters where they intersect.
    private bool IsLegal(WordData other)
    {
        var intersects = Intersect(other);
        var adjacent = Adjacent(other);
        if (!intersects)
        {
            return adjacent;
        }
        if (IsHorizontal == other.IsHorizontal && intersects)
        {
            return false;
        }
        if (IsHorizontal != other.IsHorizontal && intersects)
        {
            var intersection = Intersection(other);
            return Word[intersection.x] == other.Word[intersection.y];
        }
        return true;
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

    private static bool GenerateCrossword(WordData[] output, string[] words, int currentIndex)
    {
        // Base case: we're out of words to add
        if (currentIndex == words.Length)
            return true;


        // Inductive case: try every position and see if it's a valid placement. If so, increment the index and go one level deeper.
        for (var i = 0; i < currentIndex;i++)
        {
            output[currentIndex] = new WordData(words[currentIndex]);
            // Find all of the matching letters.
            var matches = output[currentIndex].MatchingLetters(output[i]);

            for (var j = 0; j < matches.Count; j++)
            {
                var match = matches[j];
                var startPos = output[i].GetOffset(match);
                output[currentIndex] = new WordData(words[currentIndex], startPos, !output[i].IsHorizontal);
                if (output[currentIndex].IsLegal(output, currentIndex))
                {
                    var success = GenerateCrossword(output, words, currentIndex + 1);
                    if (success)
                    {
                        return true;
                    }
                }
            }

        }

        // If we run out of things to try, notify the caller.
        return false;
    }

    public static WordData[] GenerateCrossword(string[] words)
    {
        // Words will be placed relevative to the first one, so the position and orientation can be set arbitrarily.
        var crosswords = new WordData[words.Length];
        crosswords[0] = new WordData(words[0]);
        var success = GenerateCrossword(crosswords, words, 1);
        if (!success)
            return null;
        return crosswords;
    }

}