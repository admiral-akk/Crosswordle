using NUnit.Framework;
using UnityEngine;

public class WordDataTest
{
    // Test dimensions:
    // 1. Orthogonal or parallel
    // 2. Adjacent, matching intersection, non-matching intersection, none
    [Test]
    public void WordDataTestIsLegalOrthogonalMatchingIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.zero, false);

        Assert.IsTrue(word.IsLegal(other));
        Assert.IsTrue(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalOrthogonalNonMatchingIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.right, false);

        Assert.IsFalse(word.IsLegal(other));
        Assert.IsFalse(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalOrthogonalAdjacent()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.one, false);

        Assert.IsFalse(word.IsLegal(other));
        Assert.IsFalse(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalOrthogonalNone()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", 2*Vector2Int.one, false);

        Assert.IsTrue(word.IsLegal(other));
        Assert.IsTrue(other.IsLegal(word));
    }
    [Test]
    public void WordDataTestIsLegalParallelMatchingIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("olleh", 4*Vector2Int.right, true);

        Assert.IsFalse(word.IsLegal(other));
        Assert.IsFalse(other.IsLegal(word));
    }
    [Test]
    public void WordDataTestIsLegalParallelNonMatchingIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", 4 * Vector2Int.right, true);

        Assert.IsFalse(word.IsLegal(other));
        Assert.IsFalse(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalParallelAdjacent()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("olleh", 4 * Vector2Int.right + Vector2Int.down, true);

        Assert.IsFalse(word.IsLegal(other));
        Assert.IsFalse(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalParallelNone()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("olleh", 4 * Vector2Int.right + 2*Vector2Int.down, true);

        Assert.IsTrue(word.IsLegal(other));
        Assert.IsTrue(other.IsLegal(word));
    }

    // Test dimensions:
    // 1. Multiple matching letters
    // 2. No matching letters
    // 3. Letter matches aren't symmetric with respect to index.

    [Test]
    public void WordDataTestMatchingLettersSameWord()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.zero, false);

        var expected = new Vector2Int[]
        {
            new Vector2Int(0,0),
            new Vector2Int(1,1),
            new Vector2Int(2,2),
            new Vector2Int(2,3),
            new Vector2Int(3,2),
            new Vector2Int(3,3),
            new Vector2Int(4,4)
        };

        Assert.That(word.MatchingLetters(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.MatchingLetters(word.Word), Is.EqualTo(expected).AsCollection);
    }

    [Test]
    public void WordDataTestMatchingLettersNoMatch()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("abcdf", Vector2Int.zero, false);

        var expected = new Vector2Int[]{};

        Assert.That(word.MatchingLetters(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.MatchingLetters(word.Word), Is.EqualTo(expected).AsCollection);
    }

    [Test]
    public void WordDataTestMatchingLettersAssymetricMatch()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("loser", Vector2Int.zero, false);

        var expected = new Vector2Int[]
        {
            new Vector2Int(1,3),
            new Vector2Int(2,0),
            new Vector2Int(3,0),
            new Vector2Int(4,1)
        };
        var expectedReverse = new Vector2Int[]
        {
            new Vector2Int(0,2),
            new Vector2Int(0,3),
            new Vector2Int(1,4),
            new Vector2Int(3,1),
        };

        Assert.That(word.MatchingLetters(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.MatchingLetters(word.Word), Is.EqualTo(expectedReverse).AsCollection);
    }

    // Test dimensions:
    // 1. Multiple matching letters
    // 2. No matching letters
    // 3. Letter matches aren't symmetric with respect to index.

    [Test]
    public void WordDataTestGetStartPositionsAssymetricMatch()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("loser", Vector2Int.zero, false);

        var expected = new Vector2Int[]
        {
            new Vector2Int(1,-3),
            new Vector2Int(2,0),
            new Vector2Int(3,0),
            new Vector2Int(4,-1),
        };
        var expectedReverse = new Vector2Int[]
        {
            new Vector2Int(-2,0),
            new Vector2Int(-3,0),
            new Vector2Int(-4,1),
            new Vector2Int(-1,3)
        };

        Assert.That(word.GetStartPositions(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.GetStartPositions(word.Word), Is.EqualTo(expectedReverse).AsCollection);
    }


    [Test]
    public void WordDataTestGetStartPositionsNoMatch()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("abcdf", Vector2Int.zero, false);

        var expected = new Vector2Int[] { };

        Assert.That(word.GetStartPositions(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.GetStartPositions(word.Word), Is.EqualTo(expected).AsCollection);
    }

    [Test]
    public void WordDataTestGetStartPositionSameWord()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.zero, false);

        var expected = new Vector2Int[]
        {
            new Vector2Int(0,0),
            new Vector2Int(1,-1),
            new Vector2Int(2,-2),
            new Vector2Int(2,-3),
            new Vector2Int(3,-2),
            new Vector2Int(3,-3),
            new Vector2Int(4,-4)
        };
        var expectedReverse = new Vector2Int[]
        {
            new Vector2Int(0,0),
            new Vector2Int(-1,1),
            new Vector2Int(-2,2),
            new Vector2Int(-3,2),
            new Vector2Int(-2,3),
            new Vector2Int(-3,3),
            new Vector2Int(-4,4)
        };

        Assert.That(word.GetStartPositions(other.Word), Is.EqualTo(expected).AsCollection);
        Assert.That(other.GetStartPositions(word.Word), Is.EqualTo(expectedReverse).AsCollection);
    }
}
