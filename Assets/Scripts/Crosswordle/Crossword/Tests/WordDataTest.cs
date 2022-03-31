using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WordDataTest
{
    // Test dimensions:
    // 1. Orthogonal or parallel
    // 2. Adjacent, legal intersection, illegal intersection, none
    [Test]
    public void WordDataTestIsLegalOrthogonalIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("hello", Vector2Int.zero, false);

        Assert.IsTrue(word.IsLegal(other));
        Assert.IsTrue(other.IsLegal(word));
    }

    [Test]
    public void WordDataTestIsLegalOrthogonalIllegalIntersection()
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
    public void WordDataTestIsLegalParallelIntersection()
    {
        var word = new WordData("hello", Vector2Int.zero, true);
        var other = new WordData("olleh", 4*Vector2Int.right, true);

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

}
