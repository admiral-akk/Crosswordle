using NUnit.Framework;

public class CrosswordKnowledgeTest
{
    [Test]
    public void CrosswordKnowledgeTestStartState()
    {
        var data = CrosswordData.GenerateCrossword(new Word[] { "HELLO"});
        var crossword = new CrosswordKnowledge(data);

        foreach (var problem in crossword.Problems)
        {
            for (var i = 0; i < problem.Length; i++)
            {
                var k = problem.GetKnowledge(i);
                Assert.IsFalse(k.IsSolved);
                Assert.IsEmpty(k.Hints);
            }
        }
    }

    [Test]
    public void CrosswordKnowledgeTestCorrectGuess()
    {
        var word = new Word("HELLO");
        var data = CrosswordData.GenerateCrossword(new Word[] { word });
        var crossword = new CrosswordKnowledge(data);

        crossword.Guess(word);

        foreach (var problem in crossword.Problems)
        {
            for (var i = 0; i < problem.Length; i++)
            {
                var k = problem.GetKnowledge(i);
                Assert.IsTrue(k.IsSolved);
                Assert.AreEqual(k.Answer, word[i]);
                Assert.IsEmpty(k.Hints);
            }
        }
    }

    [Test]
    public void CrosswordKnowledgeTestMultipleWords()
    {
        var word = new Word("HELLO");
        var word2= new Word("EGATD");
        var data = CrosswordData.GenerateCrossword(new Word[] { word, word2 });
        var crossword = new CrosswordKnowledge(data);

        crossword.Guess(word);

        foreach (var problem in crossword.Problems)
        {
            if (problem.GetKnowledge(3).IsSolved)
            {
                for (var i = 0; i < problem.Length; i++)
                {
                    var k = problem.GetKnowledge(i);
                    Assert.IsTrue(k.IsSolved);
                    Assert.AreEqual(k.Answer, word[i]);
                    Assert.IsEmpty(k.Hints);
                }
            } else
            {
                for (var i = 0; i < problem.Length; i++)
                {
                    var k = problem.GetKnowledge(i);
                    switch (i)
                    {
                        case 0:
                            Assert.IsTrue(k.IsSolved);
                            Assert.AreEqual(k.Answer,'E');
                            Assert.IsEmpty(k.Hints);
                            break;
                        default:
                            Assert.IsFalse(k.IsSolved);
                            Assert.IsEmpty(k.Hints);
                            break;
                    }
                }
            }
        }
    }

    [Test]
    public void CrosswordKnowledgeTestDimensionsSingleWord()
    {
        var word = new Word("HELLO");
        var data = CrosswordData.GenerateCrossword(new Word[] { word});
        var crossword = new CrosswordKnowledge(data);

        Assert.AreEqual(5, crossword.Dimensions.Item1);
        Assert.AreEqual(1, crossword.Dimensions.Item2);
    }

    [Test]
    public void CrosswordKnowledgeTestDimensionsTwoWord()
    {
        var word = new Word("HELLO");
        var word2 = new Word("HPPPP");
        var data = CrosswordData.GenerateCrossword(new Word[] { word, word2 });
        var crossword = new CrosswordKnowledge(data);

        Assert.AreEqual(5, crossword.Dimensions.Item1);
        Assert.AreEqual(5, crossword.Dimensions.Item2);
    }
}
