using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterKnowledge
{
    public string PossibleLetters;
    public KnowledgeState State;
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

    public LetterKnowledge()
    {
        State = KnowledgeState.None;
        PossibleLetters = "";
    }

    public void Update(char c, bool isCorrect)
    {
        if (State == KnowledgeState.Correct)
            return;
        if (isCorrect)
        {
            PossibleLetters = c.ToString();
            State = KnowledgeState.Correct;
            return;
        }
        PossibleLetters += c.ToString();
        State = KnowledgeState.WrongPosition;
        return;

    }
}

public  class Answer
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
        for (var i = 0; i < Knowledge.Length; i++)
        {
            Knowledge[i] = new LetterKnowledge();
        }
    }

    private Answer(LetterKnowledge[] knowledge, Vector2Int startPosition, bool isHorizontal, string answer)
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
            if (guess[i] == _answer[i])
            {
                newKnowledge[i].Update(guess[i], true);
            } 
        }
        return new Answer(newKnowledge, StartPosition, IsHorizontal, _answer);
    }

    public int Length => _answer.Length;
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

    public void HandleGuess(Word guess)
    {
        foreach (var answer in Answers)
        {
            answer.Update(guess);
        }
    }
}