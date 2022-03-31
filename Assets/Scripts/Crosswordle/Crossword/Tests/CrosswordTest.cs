using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CrosswordTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void CrosswordTestTwoWords()
    {
        var words = new string[2]
        {
            "cab","dub"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }
    [Test]
    public void CrosswordTestTwoLongWords()
    {
        var words = new string[2]
        {
            "words","culdo"
        };

        var crossword = CrosswordData.GenerateCrossword(words);

        Assert.AreEqual(2, crossword.Words.Length);
        Debug.Log(crossword.ToString());
    }
}
