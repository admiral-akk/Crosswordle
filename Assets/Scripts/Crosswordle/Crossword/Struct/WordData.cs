using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public readonly struct WordData
{
    public readonly Word Word;
    public readonly Vector2Int StartPosition;
    public readonly bool IsHorizontal;

    public WordData(Word word, Vector2Int pos, bool horizontal)
    {
        Word = word;
        StartPosition = pos;
        IsHorizontal = horizontal;
    }
    public WordData(Word word)
    {
        Word = word;
        StartPosition = Vector2Int.zero;
        IsHorizontal = true;
    }

    public char this[int i] => Word[i];

    // Gets legal start positions for this word.
    public List<Vector2Int> GetStartPositions(Word word)
    {
        var matches = MatchingLetters(word);
        var isHorizontal = IsHorizontal;
        var startPos = StartPosition;
        return matches.Select(match =>
        {
            if (isHorizontal)
            {
                return startPos + new Vector2Int(match.x, -match.y);
            } else
            {
                return startPos + new Vector2Int( -match.y, match.x);
            }
        }).ToList();
    }

    public List<Vector2Int> MatchingLetters(Word word)
    {
        var matches = new List<Vector2Int>();
        for (var i = 0; i < Word.Length; i++)
        {
            for (var j = 0; j < word.Length; j++)
            {
                if (Word[i] == word[j])
                {
                    matches.Add(new Vector2Int(i, j));
                }
            }
        }
        return matches;
    }

    public bool IsLegal(WordData other)
    {
        var adjacent = Adjacent(other);

        // If they aren't adjacent, they don't interact.
        if (!adjacent)
            return true;

        // If they are adjacent and parallel, it's illegal.
        if (IsHorizontal == other.IsHorizontal)
            return false;

        var intersects = Intersect(other);

        // If they are adjacent, orthogonal, and don't intersect, it's illegal.
        if (!intersects)
            return false;

        var intersection = Intersection(other);

        // If they are adjacent, orthogonal, and intersect, then the letters should match.
        if (IsHorizontal)
            return Word[intersection.x - StartPosition.x] == other.Word[intersection.y - other.StartPosition.y];
        else
            return Word[intersection.y - StartPosition.y] == other.Word[intersection.x - other.StartPosition.x];
    }

    private bool Adjacent(Vector2Int point)
    {
        var xDelta = point.x - StartPosition.x;
        var yDelta = point.y - StartPosition.y;
        var isAdjacent = yDelta > -2 && xDelta > -2;

        if (IsHorizontal)
        {
            isAdjacent &= yDelta < 2 && xDelta < Word.Length + 1;
        }
        else
        {
            isAdjacent &= xDelta < 2 && yDelta < Word.Length + 1;
        }
        return isAdjacent;
    }

    private bool Adjacent(WordData other)
    {
        if (IsHorizontal == other.IsHorizontal)
        {
            return Adjacent(other.StartPosition) || other.Adjacent(StartPosition);
        } else if (!IsHorizontal)
        {
            return other.Adjacent(this);
        }
        var intersectionPoint = Intersection(other);
        return Adjacent(intersectionPoint) && other.Adjacent(intersectionPoint);
    }

    // We assume that the two words are orthogonal.
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
            return other.Intersection(this);
        return new Vector2Int(other.StartPosition.x, StartPosition.y);
    }
}