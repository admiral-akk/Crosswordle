using System.Collections.Generic;
using System.Linq;
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