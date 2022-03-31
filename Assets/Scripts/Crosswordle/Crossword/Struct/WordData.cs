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
        return Word[intersection.x] == other.Word[intersection.y];
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