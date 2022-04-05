
using System.Collections.Generic;

public class GuessKnowledge
{
    private class GuessCharacterKnowledge
    {
        public enum Knowledge
        {
            None,
            Hinted,
            Solved,
            Incomplete
        }
        private readonly HashSet<char> _solved;
        private readonly HashSet<char> _hints;
        public GuessCharacterKnowledge()
        {
            _solved = new HashSet<char>();
            _hints = new HashSet<char>();
        }

        public void AddSolved(char c)
        {
            _solved.Add(c);
        }
        public void AddHints(HashSet<char> hints)
        {
            _hints.UnionWith(hints);
        }

        public Knowledge Get(char c)
        {
            var solved = _solved.Contains(c);
            var hinted = _hints.Contains(c);
            if (solved && hinted)
            {
                return Knowledge.Incomplete;
            } else if (solved)
            {
                return Knowledge.Solved;
            } else if (hinted)
            {
                return Knowledge.Hinted;
            } else
            {
                return Knowledge.None;
            }
        }
    }
    private GuessCharacterKnowledge _wordKnowledge;
    public enum Knowledge
    {
        Unused,
        NotHere,
        NotInCrossword,
        CouldBeHere,
        Complete,
    }
    private GuessCharacterKnowledge[] _knowledge;
    private List<Word> _guesses;

    public GuessKnowledge(int wordLength)
    {
        _knowledge = new GuessCharacterKnowledge[wordLength];
        _wordKnowledge = new GuessCharacterKnowledge();
        for (var i = 0; i < _knowledge.Length; i++)
        {
            _knowledge[i] = new GuessCharacterKnowledge();
        }
    }

    public void AddCorrect(char c, int index)
    {
        _wordKnowledge.AddSolved(c);
        _knowledge[index].AddSolved(c);
    }
    public void AddHints(HashSet<char> hints, int index)
    {
        _wordKnowledge.AddHints(hints);
        _knowledge[index].AddHints(hints);
    }

    public void AddGuesses(List<Word> guesses)
    {
        _guesses = new List<Word>(guesses);
    }

    public Knowledge Get(char c, int i)
    {
        var usedLetters = new HashSet<char>();
        foreach (var guess in _guesses)
        {
            foreach (var guessLetter in guess.ToString())
            {
                usedLetters.Add(guessLetter);
            }
        }
        if (!usedLetters.Contains(c))
            return Knowledge.Unused;
        if (_wordKnowledge.Get(c) == GuessCharacterKnowledge.Knowledge.None)
            return Knowledge.NotInCrossword;
        if (_wordKnowledge.Get(c) == GuessCharacterKnowledge.Knowledge.Solved)
            return Knowledge.Complete;

        var g = _knowledge[i].Get(c);

        switch (g)
        {
            case GuessCharacterKnowledge.Knowledge.Incomplete:
            case GuessCharacterKnowledge.Knowledge.Solved:
                return Knowledge.Complete;
            case GuessCharacterKnowledge.Knowledge.Hinted:
                return Knowledge.CouldBeHere;
            default:
                return Knowledge.NotHere;
        }
    }
}