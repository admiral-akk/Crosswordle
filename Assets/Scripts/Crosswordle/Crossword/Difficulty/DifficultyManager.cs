using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private int StartingWordCount;
    [SerializeField] private int StartingWordLength;

    public int WordCount
    {
        get;
        private set;
    }
    public int WordLength
    {
        get;
        private set;
    }

    public void FreshRun()
    {
        WordCount = StartingWordCount;
        WordLength = StartingWordLength;
    }

    private void OnWin()
    {
        WordCount++;
    }

    private void OnLoss()
    {
        FreshRun();
    }
}
