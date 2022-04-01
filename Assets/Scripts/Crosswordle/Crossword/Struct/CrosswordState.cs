using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public readonly struct LetterKnowledge
{
    public readonly string PossibleLetters;
    public readonly KnowledgeState State;
    public enum KnowledgeState
    {
        None,
        WrongPosition,
        Correct
    }

    public LetterKnowledge(string possibleLetters, KnowledgeState state)
    {
        PossibleLetters = possibleLetters;
        State = state;
    }
}

public readonly struct Answer
{
    public readonly LetterKnowledge[] Knowledge;
    public readonly Vector2Int StartPosition;
    public readonly bool IsHorizontal;
    private readonly string _answer;

    public Answer(WordData data)
    {
        _answer = data.Word;
        IsHorizontal = data.IsHorizontal;
        StartPosition = data.StartPosition;
        Knowledge = new LetterKnowledge[data.Word.Length];
    }

    private Answer(LetterKnowledge[] knowledge, Vector2Int startPosition, bool isHorizontal, string answer)
    {
        _answer = answer;
        IsHorizontal = isHorizontal;
        StartPosition = startPosition;
        Knowledge = knowledge;
    }

    public Answer Update(string guess)
    {
        var newKnowledge = Knowledge;
        for (var i = 0; i < guess.Length; i++)
        {
            if (guess[i] == _answer[i])
            {
                newKnowledge[i] = new LetterKnowledge(guess[i].ToString(), LetterKnowledge.KnowledgeState.Correct);
            }
        }
        return new Answer(newKnowledge, StartPosition, IsHorizontal, _answer);
    }
}
public class CrosswordState
{
    public List<Answer> Answers;

    public (int, int) Dimensions => (Answers.Select(a => a.StartPosition.x).Max(), Answers.Select(a => a.StartPosition.y).Max());

    public CrosswordState(CrosswordData data)
    {
        Answers = new List<Answer>(data.Words.Length);
        foreach (var datum in data.Words)
        {
            Answers.Add(new Answer(datum));
        }
    }
}