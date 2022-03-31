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
            match = new Vector2Int(match.y, -match.x);
            return StartPosition + match;
        }
        else
        {
            match = new Vector2Int(-match.x, match.y);
            return StartPosition + match;
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
        }
        else
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
        return Adjacent(intersectionPoint) && other.Adjacent(intersectionPoint);
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
            return other.Intersection(this);
        return new Vector2Int(other.StartPosition.x, StartPosition.y);
    }

    // Placement is illegal if:
    // 1. The two words are adjacent and don't intersect.
    // 2. The two words are parallel and intersect.
    // 3. The two words are orthogonal, intersect, and have different characters where they intersect.
    public bool IsLegal(WordData other)
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
}