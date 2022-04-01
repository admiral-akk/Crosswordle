
using NUnit.Framework;
using UnityEngine;
using static LetterKnowledge;

public class AnswerTest
{
    [Test]
    public void AnswerTestStartState()
    {
        var word = new WordData("Hello");
        var answer = new Answer(word);

        Assert.AreEqual(5, answer.Length);
        for (var  i = 0; i < 5; i++)
        {
            Assert.AreEqual("", answer.Knowledge[i].PossibleLetters);
            Assert.AreEqual(KnowledgeState.None, answer.Knowledge[i].State);
        }
    }


    [Test]
    public void AnswerTestBadGuess()
    {
        var word = new WordData("Hello");
        var answer = new Answer(word);

        answer.Update(new Word("fatty"));

        Assert.AreEqual(5, answer.Length);
        for (var i = 0; i < 5; i++)
        {
            Assert.AreEqual("", answer.Knowledge[i].PossibleLetters);
            Assert.AreEqual(KnowledgeState.None, answer.Knowledge[i].State);
        }
    }


    [Test]
    public void AnswerTestCorrectGuess()
    {
        var word = new WordData("hello");
        var answer = new Answer(word);

        answer.Update(new Word("hello"));

        Assert.AreEqual(5, answer.Length);
        for (var i = 0; i < 5; i++)
        {
            Assert.AreEqual(word[i].ToString(), answer.Knowledge[i].PossibleLetters);
            Assert.AreEqual(KnowledgeState.Correct, answer.Knowledge[i].State);
        }
    }

    [Test]
    public void AnswerTestWrongPositionGuess()
    {
        var word = new WordData("Hello");
        var answer = new Answer(word);

        answer.Update(new Word("ghent"));

        Assert.AreEqual(5, answer.Length);
        for (var i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 1:
                    Assert.AreEqual("E", answer.Knowledge[i].PossibleLetters);
                    break;
                case 2:
                    Assert.AreEqual("H", answer.Knowledge[i].PossibleLetters);
                    break;
                default:
                    Assert.AreEqual("HE", answer.Knowledge[i].PossibleLetters);
                    break;
            }
            Assert.AreEqual(KnowledgeState.WrongPosition, answer.Knowledge[i].State);
        }
    }

    [Test]
    public void AnswerTestMultipleWrongPositionGuess()
    {
        var word = new WordData("Hello");
        var answer = new Answer(word);

        answer.Update(new Word("ghent"));
        answer.Update(new Word("clock"));

        Assert.AreEqual(5, answer.Length);
        for (var i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 1:
                    Assert.AreEqual("EO", answer.Knowledge[i].PossibleLetters);
                    break;
                case 2:
                    Assert.AreEqual("HL", answer.Knowledge[i].PossibleLetters);
                    break;
                default:
                    Assert.AreEqual("HELO", answer.Knowledge[i].PossibleLetters);
                    break;
            }
            Assert.AreEqual(KnowledgeState.WrongPosition, answer.Knowledge[i].State);
        }
    }
}
