// Crossword consists of problems

// Problems consist of an answer, knowledge, a position, and other intersecting problems
// Knowledge tells you whether a letter is known, and if not, what guess have been applied
using System.Collections.Generic;
using System.Linq;

public class CrosswordKnowledge
{
    public List<ProblemKnowledge> Problems;

    private int xDim, yDim;
    public (int, int) Dimensions => (xDim,yDim);

    public CrosswordKnowledge(CrosswordData data)
    {
        Problems = new List<ProblemKnowledge>(data.Words.Length);
        foreach (var word in data.Words)
        {
            var problem = new ProblemKnowledge(word);
            for (var i = 0; i < Problems.Count; i++)
                Problems[i].UpdateIntersection(problem);
            Problems.Add(problem);
        }
        foreach (var problem in Problems)
        {
            problem.InitializeKnowledgeGraph();
        }
        xDim = Problems.Select(p => p.X + (p.IsHorizontal ? p.Length : 1)).Max();
        yDim = Problems.Select(p => p.Y  + (!p.IsHorizontal ? p.Length : 1)).Max();
    }

    public void Guess(Word guess)
    {
        foreach (var problem in Problems)
        {
            problem.Guess(guess);
        }
    }

    public void Spoil()
    {
        foreach (var problem in Problems)
        {
            problem.Spoil();
        }
    }
}
