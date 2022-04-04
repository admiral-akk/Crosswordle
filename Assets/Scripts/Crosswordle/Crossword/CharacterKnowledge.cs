
using System.Collections.Generic;

public class CharacterKnowledge
{
    public enum Knowledge
    {
        None,
        NotInCrossword,
        Incomplete,
        Complete,
    }

    private Dictionary<char, Knowledge> _knowledge;
    public CharacterKnowledge()
    {
        _knowledge = new Dictionary<char, Knowledge>();
    }

    public CharacterKnowledge(CharacterKnowledge other)
    {
        _knowledge = new Dictionary<char, Knowledge>(other._knowledge);
    }

    public Knowledge Get(char c)
    {
        if (_knowledge.ContainsKey(c))
            return _knowledge[c];
        return Knowledge.None;
    }

    public void Guess(Word word)
    {
        foreach (var c in word.ToString())
        {
            if (!_knowledge.ContainsKey(c))
                _knowledge[c] = Knowledge.NotInCrossword;
        }
    }

    public void Update(LetterKnowledgeState knowledge)
    {
        if (knowledge.IsSolved)
        {
            var c = knowledge.Answer.Value;
            if (_knowledge[c] != Knowledge.Incomplete)
                _knowledge[c] = Knowledge.Complete;
        } else
        {
            foreach (var c in knowledge.Hints)
            {
                _knowledge[c] = Knowledge.Incomplete;
            }
        }
    }

    public void SetWrong(char c)
    {
        _knowledge[c] = Knowledge.NotInCrossword;
    }
}