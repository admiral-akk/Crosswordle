using System.Collections.Generic;

// Knowledge needs to be able to:
// 1. Say which letters we know (are green)
// 2. For the letters we don't know, what are the list of potential letters
// 3. If we update a letter's knowledge, we need to be able to share that with the intersecting problem
public class ProblemLetterKnowledge
{
    private char _answer;
    private HashSet<char> _guesses;
    private HashSet<char> _possible;
    private HashSet<char> _impossible;
    public ProblemLetterKnowledge(char answer)
    {
        _guesses = new HashSet<char>();
        _possible = new HashSet<char>();
        _impossible = new HashSet<char>();
        _answer = answer;
    }
    public IEnumerable<char> Hints
    {
        get
        {
            var hints = new HashSet<char>();
            hints.UnionWith(_possible);
            hints.ExceptWith(_impossible);
            hints.ExceptWith(_guesses);
            return hints;
        }
    }

    public LetterKnowledgeState State
    {
        get
        {
            if (Solved)
                return new LetterKnowledgeState( Hints, _answer);
            return new LetterKnowledgeState(Hints);
        }
    }

    public bool Solved => _guesses.Contains(_answer);
    public void Eliminate(char guess)
    {
        _impossible.Add(guess);
    }

    public void AddHint(char guess)
    {
        _possible.Add(guess);
    }
    public void Guess(char guess)
    {
        _guesses.Add(guess);
    }
}

public readonly struct LetterKnowledgeState
{
    public bool IsSolved => Answer.HasValue;
    public readonly char? Answer;
    public readonly IEnumerable<char> Hints;
    public LetterKnowledgeState(IEnumerable<char> hints, char? answer = null)
    {
        Hints = hints;
        Answer = answer;
    }
}