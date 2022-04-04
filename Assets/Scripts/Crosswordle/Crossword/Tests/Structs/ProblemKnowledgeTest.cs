
using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class ProblemKnowledgeTest
{
    [Test]
    public void ProblemKnowledgeTestStartState()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        for (var i = 0; i < word.Word.Length; i++)
        {
           var k = knowledge.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            Assert.IsEmpty(k.Hints);
        }
    }

    [Test]
    public void ProblemKnowledgeTestCorrectGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("ABATE");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsTrue(k.IsSolved);
            Assert.AreEqual(k.Answer, word.Word[i]);
            Assert.IsEmpty(k.Hints);
        }
    }

    [Test]
    public void ProblemKnowledgeTestCompletelyWrongGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("HORNY");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            Assert.IsEmpty(k.Hints);
        }
    }

    [Test]
    public void ProblemKnowledgeTestWrongPositionGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("EIGHT");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            switch (i)
            {
                case 0:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'T' }).AsCollection);
                    break;
                case 4:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'E' }).AsCollection);
                    break;
                default:
                    Assert.That(k.Hints, Is.EqualTo(new char[2]{ 'E', 'T'}).AsCollection);
                    break;
            }
        }
    }

    [Test]
    public void ProblemKnowledgeTestMultipleWrongPositionGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("TIGHT");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            switch (i)
            {
                case 0:
                case 4:
                    Assert.IsEmpty(k.Hints);
                    break;
                default:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'T' }).AsCollection);
                    break;
            }
        }
    }

    [Test]
    public void ProblemKnowledgeTestRightPositionGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("TITTY");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsEmpty(k.Hints);
            switch (i)
            {
                case 3:
                    Assert.IsTrue(k.IsSolved);
                    Assert.AreEqual(k.Answer, 'T');
                    break;
                default:
                    Assert.IsFalse(k.IsSolved);
                    break;
            }
        }
    }
    
    [Test]
    public void ProblemKnowledgeTestRightPositionWrongPositionGuess()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("ARRAY");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            switch (i)
            {
                case 0:
                    Assert.IsTrue(k.IsSolved);
                    Assert.AreEqual(k.Answer, 'A');
                    break;
                default:
                    Assert.IsFalse(k.IsSolved);
                    break;
            }
            switch (i)
            {
                case 0:
                case 3:
                    Assert.IsEmpty(k.Hints);
                    break;
                default:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'A' }).AsCollection);
                    break;
            }
        }
    }

    [Test]
    public void ProblemKnowledgeTestEliminatedLetterRemovedEverywhere()
    {
        var word = new WordData("ABATE");
        var knowledge = new ProblemKnowledge(word);
        knowledge.InitializeKnowledgeGraph();

        knowledge.Guess("ARRAY");
        knowledge.Guess("GRAPE");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsEmpty(k.Hints);
            switch (i)
            {
                case 0:
                case 2:
                case 4:
                    Assert.IsTrue(k.IsSolved);
                    break;
                default:
                    Assert.IsFalse(k.IsSolved);
                    break;
            }
            switch (i)
            {
                case 0:
                case 2:
                    Assert.AreEqual(k.Answer, 'A');
                    break;
                case 4:
                    Assert.AreEqual(k.Answer, 'E');
                    break;
                default:
                    break;
            }
        }
    }

    [Test]
    public void ProblemKnowledgeTestIntersectingWordsShareSolved()
    {
        var word = new WordData("ABATE");
        var word2 = new WordData("ABATE", 2*Vector2Int.right, false);
        var knowledge = new ProblemKnowledge(word);
        var knowledge2 = new ProblemKnowledge(word2);
        knowledge.UpdateIntersection(knowledge2);
        knowledge.InitializeKnowledgeGraph();
        knowledge2.InitializeKnowledgeGraph();

        knowledge.Guess("ARRAY");
        knowledge2.Guess("ARRAY");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsEmpty(k.Hints);
            switch (i)
            {
                case 0:
                case 2:
                    Assert.IsTrue(k.IsSolved);
                    Assert.AreEqual(k.Answer, 'A');
                    break;
                default:
                    Assert.IsFalse(k.IsSolved);
                    break;
            }
        }
        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge2.GetKnowledge(i);
            switch (i)
            {
                case 0:
                    Assert.IsTrue(k.IsSolved);
                    Assert.AreEqual(k.Answer, 'A');
                    break;
                default:
                    Assert.IsFalse(k.IsSolved);
                    break;
            }
            switch (i)
            {
                case 1:
                case 2:
                case 4:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'A' }).AsCollection);
                    break;
                default:
                    Assert.IsEmpty(k.Hints);
                    break;
            }
        }
    }

    [Test]
    public void ProblemKnowledgeTestIntersectingWordsShareEliminatedHints()
    {
        var word = new WordData("ABATE");
        var word2 = new WordData("ABATE", 2 * Vector2Int.right, false);
        var knowledge = new ProblemKnowledge(word);
        var knowledge2 = new ProblemKnowledge(word2);
        knowledge.UpdateIntersection(knowledge2);
        knowledge.InitializeKnowledgeGraph();
        knowledge2.InitializeKnowledgeGraph();

        knowledge.Guess("TIGHT");
        knowledge2.Guess("TIGHT");

        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            switch (i)
            {
                case 0:
                case 2:
                case 4:
                    Assert.IsEmpty(k.Hints);
                    break;
                default:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'T' }).AsCollection);
                    break;
            }
        }
        for (var i = 0; i < word.Word.Length; i++)
        {
            var k = knowledge2.GetKnowledge(i);
            Assert.IsFalse(k.IsSolved);
            switch (i)
            {
                case 0:
                case 4:
                    Assert.IsEmpty(k.Hints);
                    break;
                default:
                    Assert.That(k.Hints, Is.EqualTo(new char[1] { 'T' }).AsCollection);
                    break;
            }
        }
    }
}
