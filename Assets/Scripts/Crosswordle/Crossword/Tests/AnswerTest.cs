
using NUnit.Framework;
using System;
using UnityEngine;

public class CrosswordTest
{
    // Test cases:
    [Test]
    public void CrosswordTestSimple1()
    {
        var words = new Word[2]
        {
            "aaa","aaa"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }

    [Test]
    public void CrosswordTestSimple2()
    {
        var words = new Word[2]
        {
            "ab","ba"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }

    [Test]
    public void CrosswordTestSimple3()
    {
        var words = new Word[2]
        {
            "hello","loser"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }

    [Test]
    public void CrosswordTestSimpleDifferentLengths()
    {
        var words = new Word[2]
        {
            "math","different"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }

    [Test]
    public void CrosswordTestSimpleNoCrossword()
    {
        var words = new Word[2]
        {
            "yolo","different"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.IsNull(crossword);
    }

    [Test]
    public void CrosswordTestComplexNoCrossword1()
    {
        // Three words only share a single letter, impossible to construct a crossword
        var words = new Word[3]
        {
            "bob", "dog", "fro"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.IsNull(crossword);
    }

    [Test]
    public void CrosswordTestComplexNoCrossword2()
    {
        // Two words need to connect to bod, but will be adjacent to each other.
        var words = new Word[3]
        {
            "bod", "orange", "dyu"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.IsNull(crossword);
    }

    [Test]
    public void CrosswordTestComplex1()
    {
        // Two words need to connect to bod, but will be adjacent to each other.
        var words = new Word[3]
        {
            "bod", "orange", "dyug"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(3, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }

}
