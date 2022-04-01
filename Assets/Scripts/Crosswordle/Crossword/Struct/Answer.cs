using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterKnowledge
{
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
    }

    public void Update(char c, bool isCorrect)
    {
        if (State == KnowledgeState.Correct)
            return;
        if (isCorrect)
        {
            Possible.Clear();
            Possible.Add(c);
            State = KnowledgeState.Correct;
            return;
        }
        Possible.Add(c);
        State = KnowledgeState.WrongPosition;
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
            if (guess[i] == _answer[i])
            {
                newKnowledge[i].Update(guess[i], true);
            }
            else if (Enumerable.Range(0, _answer.Length).Any(index => _answer[index] == guess[i] && _answer[index] != guess[index]))
            {
                for (var j = 0; j < _answer.Length; j ++)
                {
                    if (j == i)
                        continue;
                    newKnowledge[j].Update(guess[i], false);
                }
            }
        }
        return new Answer(newKnowledge, StartPosition, IsHorizontal, _answer);
    }

    public int Length => _answer.Length;
}