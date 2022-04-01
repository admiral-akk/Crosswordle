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
        }
        SetImpossible(c);
        for (var i = 0; i < guess.Length; i++)
        {
            if (answer.Contains(guess[i]))
                SetPossible(guess[i]);
            else
                SetImpossible(guess[i]);
        }
        return;
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
        return new Answer(newKnowledge, StartPosition, IsHorizontal, _answer);
    }

    public int Length => _answer.Length;
}