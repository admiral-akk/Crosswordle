
using NUnit.Framework;
public class ProblemLetterKnowledgeTest
{
    [Test]
    public void ProblemLetterKnowledgeTestStartState()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        Assert.IsFalse(knowledge.Solved);
        Assert.IsEmpty(knowledge.Hints);
    }

    [Test]
    public void ProblemLetterKnowledgeTestSolvedWithCorrectGuess()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.Guess('A');

        var state = knowledge.State;

        Assert.IsTrue(state.IsSolved);
        Assert.AreEqual(state.Answer.Value, 'A');
    }


    [Test]
    public void ProblemLetterKnowledgeTestAddHint()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.AddHint('B');
        var expected = new char[] { 'B' };

        var state = knowledge.State;

        Assert.IsFalse(state.IsSolved);
        Assert.That(state.Hints, Is.EqualTo(expected).AsCollection);
    }


    [Test]
    public void ProblemLetterKnowledgeTestAddHints()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.AddHint('B');
        knowledge.AddHint('C');
        var expected = new char[] { 'B' , 'C'};

        var state = knowledge.State;

        Assert.IsFalse(state.IsSolved);
        Assert.That(state.Hints, Is.EqualTo(expected).AsCollection);
    }


    [Test]
    public void ProblemLetterKnowledgeTestEliminatHint()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.AddHint('B');
        knowledge.AddHint('C');
        knowledge.Eliminate('B');
        var expected = new char[] { 'C' };

        var state = knowledge.State;

        Assert.IsFalse(state.IsSolved);
        Assert.That(state.Hints, Is.EqualTo(expected).AsCollection);
    }

    [Test]
    public void ProblemLetterKnowledgeTestEliminateHintBeforeHintGiven()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.Eliminate('B');
        knowledge.AddHint('B');
        knowledge.AddHint('C');
        var expected = new char[] { 'C' };

        var state = knowledge.State;

        Assert.IsFalse(state.IsSolved);
        Assert.That(state.Hints, Is.EqualTo(expected).AsCollection);
    }

    [Test]
    public void ProblemLetterKnowledgeTestSolveWithHints()
    {
        var knowledge = new ProblemLetterKnowledge('A');

        knowledge.Eliminate('B');
        knowledge.AddHint('B');
        knowledge.AddHint('C');
        knowledge.Guess('A');
        var expected = new char[] { 'C' };

        var state = knowledge.State;

        Assert.IsTrue(state.IsSolved);
        Assert.AreEqual(state.Answer.Value, 'A');
        Assert.That(state.Hints, Is.EqualTo(expected).AsCollection);
    }
}
