using System.Collections.Generic;

public class KeyboardHints
{ public enum State
    {
       None,
       OnBoard,
       Complete
        
    }
    public Dictionary<char, State> Hints;
    public KeyboardHints()
    {
        Hints = new Dictionary<char, State>();
    }

    public void Update(LetterKnowledgeState knowledge)
    {
        if (knowledge.IsSolved)
        {
            var c = knowledge.Answer.Value;
            if (!Hints.ContainsKey(c) || Hints[c] != State.OnBoard)
            {
                Hints[knowledge.Answer.Value] = State.Complete;
            }
        } else
        {
            foreach (var c in knowledge.Hints)
            {
                Hints[c] = State.OnBoard;
            }
        }
    }
}