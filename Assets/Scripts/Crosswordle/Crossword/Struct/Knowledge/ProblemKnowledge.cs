using System.Linq;
using UnityEngine;

public class ProblemKnowledge
{
    private readonly struct PositionStruct
    {
        public readonly bool IsHorizontal;
        public readonly int x, y;

        public PositionStruct(Vector2Int position, bool isHorizontal)
        {
            (x, y) = (position.x, position.y);
            IsHorizontal = isHorizontal;
        }
    }
    private PositionStruct Position;
    private Word Answer;

    public ProblemKnowledge[] Intersections;
    private ProblemLetterKnowledge[] _letterKnowledge;

    public bool IsHorizontal => Position.IsHorizontal;
    public int X => Position.x;
    public int Y => Position.y;
    public Vector2Int StartPosition => new Vector2Int(X, Y);
    public int Length => Answer.Length;

    public ProblemKnowledge(WordData data)
    {
        Position = new PositionStruct(data.StartPosition, data.IsHorizontal);
        Answer = data.Word;
        Intersections = new ProblemKnowledge[Length];
        _letterKnowledge = new ProblemLetterKnowledge[Answer.Length];
    }

    public void UpdateIntersection(ProblemKnowledge other)
    {
        if (IsHorizontal == other.IsHorizontal)
            return;
        if (!IsHorizontal)
        {
            other.UpdateIntersection(this);
            return;
        }
        if (other.X < X)
            return;
        if (other.X >= X + Length)
            return;
        if (Y < other.Y)
            return;
        if (Y >= other.Y + other.Length)
            return;
        Intersections[other.X - X] = other;
        other.Intersections[Y - other.Y] = this;
    }

    public void InitializeKnowledgeGraph()
    {
        for (var i = 0; i < Answer.Length; i++)
        {
            if (_letterKnowledge[i] != null)
                continue;
            _letterKnowledge[i] = new ProblemLetterKnowledge(Answer[i]);
            if (Intersections[i] == null)
                continue;
            Intersections[i].ShareKnowledge(this, _letterKnowledge[i]);
        }
    }

    private void ShareKnowledge(ProblemKnowledge other, ProblemLetterKnowledge knowledge)
    {
        for (var i = 0; i < Intersections.Length; i++)
        {
            if (Intersections[i] != other)
                continue;
            _letterKnowledge[i] = knowledge;
            return;
        }
        throw new System.Exception("Intersecting problem not found!");
    }

    private void UpdateEliminated()
    {
        for (var i = 0; i < Answer.Length; i++) {
            var c = Answer[i];
            if (!Enumerable.Range(0, Answer.Length).Any(index => Answer[index] == c && !_letterKnowledge[index].Solved))
            {
                foreach (var knowledge in _letterKnowledge)
                {
                    knowledge.Eliminate(c);
                }
                continue;
            }
        }
    }

    public void Guess(Word guess)
    {
        // First update each letter to indicate we guessed it
        for (var i = 0; i < Answer.Length; i++)
        {
            _letterKnowledge[i].Guess(guess[i]);
        }

        // For each letter, if the guess is wrong, but there are still characters to guess, update the hints
        for (var i = 0; i < Answer.Length; i++)
        {
            var c = guess[i];
            // For each letter, if the guess isn't in the word, mark it impossible.
            if (!Answer.Contains(c))
            {
                foreach (var knowledge in _letterKnowledge)
                {
                    knowledge.Eliminate(c);
                }
            } else
            {
                foreach (var knowledge in _letterKnowledge)
                {
                    knowledge.AddHint(c);
                }
            }
        }

        UpdateEliminated();
        for (var i = 0; i < Answer.Length; i++)
        {
            if (Intersections[i] == null)
                continue;
            Intersections[i].UpdateEliminated();
        }
    }

    public LetterKnowledgeState GetKnowledge(int index)
    {
        return _letterKnowledge[index].State;
    }
}