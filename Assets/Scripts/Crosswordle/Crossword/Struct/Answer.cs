using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterKnowledge
{
    private HashSet<char> Impossible;
    private HashSet<char> Possible;
    public string PossibleLetters
    {
        get
        {
            var s = "";
            foreach (var c in Possible)
            {
                s += c.ToString();
            }
            return s;

        }
    }
    public KnowledgeState State;
    public enum KnowledgeState
    {
        None,
        WrongPosition,
        Correct
    }

    public LetterKnowledge()
    {
        State = KnowledgeState.None;
        Possible = new HashSet<char>();
        Impossible = new HashSet<char>();
    }

    private void SetPossible(char c)
    {
        if (Impossible.Contains(c))
            return;
        Possible.Add(c);
        State = KnowledgeState.WrongPosition;
    }

    private void SetImpossible(char c)
    {
        Impossible.Add(c);
        Possible.Remove(c);
        if (Possible.Count == 0)
            State = KnowledgeState.None;
    }

    public void Update(int index, Word guess, Word answer)
    {
        if (State == KnowledgeState.Correct)
            return;
        var c = guess[index];
        if (c == answer[index])
        {
            Possible.Clear();
            Possible.Add(c);
            State = KnowledgeState.Correct;
            return;
        } else if (answer.ToString().Any(ch => ch == c))
        {
            Possible.Add(c);
            State = KnowledgeState.WrongPosition;
        }
        return;
    }

    public void ClearCompleteLetter(char c)
    {
        if (State == KnowledgeState.Correct)
            return;
        SetImpossible(c);
    }

    public static LetterKnowledge Intersection(LetterKnowledge knowledge, LetterKnowledge other)
    {
        var ret = new LetterKnowledge();
        ret.Impossible.UnionWith(other.Impossible);
        ret.Impossible.UnionWith(knowledge.Impossible);
        ret.Possible.UnionWith(knowledge.Possible);
        ret.Possible.IntersectWith(other.Possible);
        if (knowledge.State == KnowledgeState.Correct)
        {
            ret.State = KnowledgeState.Correct;
        }
        else if (ret.Possible.Count > 0)
        {
            ret.State = KnowledgeState.WrongPosition;
        }
        else
        {
            ret.State = KnowledgeState.None;
        }
        return ret;
    }
}

public class Answer
{
    public readonly LetterKnowledge[] Knowledge;
    public readonly Vector2Int StartPosition;
    public readonly bool IsHorizontal;
    private readonly Word _answer;

    public Answer(WordData data)
    {
        _answer = data.Word;
        IsHorizontal = data.IsHorizontal;
        StartPosition = data.StartPosition;
        Knowledge = new LetterKnowledge[data.Word.Length];
        for (var i = 0; i < Knowledge.Length; i++)
        {
            Knowledge[i] = new LetterKnowledge();
        }
    }

    private Answer(LetterKnowledge[] knowledge, Vector2Int startPosition, bool isHorizontal, Word answer)
    {
        _answer = answer;
        IsHorizontal = isHorizontal;
        StartPosition = startPosition;
        Knowledge = knowledge;
    }

    public Answer Update(Word guess)
    {
        var newKnowledge = Knowledge;
        for (var i = 0; i < guess.Length; i++)
        {
            newKnowledge[i].Update(i, guess, _answer);
        }
        for (var i = 0; i < _answer.Length; i++)
        {
            if (Knowledge[i].State != LetterKnowledge.KnowledgeState.Correct)
                continue;
            var allInstanceKnown = true;
            for (var j = 0; j < _answer.Length; j++)
            {
                if (_answer[i] != _answer[j])
                    continue;
                if (Knowledge[i].State == LetterKnowledge.KnowledgeState.Correct)
                    continue;
                allInstanceKnown = false;
                break;
            }
            if (allInstanceKnown)
            {
                foreach (var knowledge in Knowledge)
                {
                    knowledge.ClearCompleteLetter(_answer[i]);
                }
            }
        }
        return new Answer(newKnowledge, StartPosition, IsHorizontal, _answer);
    }

    public bool Intersects(Answer other)
    {
        var (i1, i2) = IntersectionIndices(other);
        if (i1 < 0)
            return false;
        if (i2 < 0)
            return false;
        if (i1 >= Length)
            return false;
        if (i2 >= other.Length)
            return false;
        return true;
    }

    private (int, int) IntersectionIndices(Answer other)
    {
        if (other.IsHorizontal == IsHorizontal)
            return (-1,-1);
        if (!IsHorizontal)
        {
            var (i1,i2) = other.IntersectionIndices(this);
            return (i2, i1);
        }
        var intersectionPoint = new Vector2Int(other.StartPosition.x, StartPosition.y);
        var xDelta = intersectionPoint.x - StartPosition.x;
        var yDelta = intersectionPoint.y - other.StartPosition.y;
        return (xDelta, yDelta);
    }

    public void UpdateIntersection(Answer other)
    {
        var (i1, i2) = IntersectionIndices(other);
        var (k1, k2) = (Knowledge[i1], other.Knowledge[i2]);
        Knowledge[i1] = LetterKnowledge.Intersection(k1, k2);
        Knowledge[i2] = LetterKnowledge.Intersection(k1, k2);
    }

    public int Length => _answer.Length;
}