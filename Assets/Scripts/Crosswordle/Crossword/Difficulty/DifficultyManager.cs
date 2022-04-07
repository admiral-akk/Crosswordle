using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField, Range(3, 10)] private int StartingWordCount;
    [SerializeField, Range(3, 10)] private int StartingWordLength;
    [SerializeField, Range(3, 10)] private int StartingGuessCount;

    public int WordCount {  get;  private set; }
    public int WordLength {  get;  private set;  }

    public int GuessCount { get; private set; }

    public void FreshRun()
    {
        WordCount = StartingWordCount;
        WordLength = StartingWordLength;
        GuessCount = StartingGuessCount;
    }

    public void OnWin()
    {
        WordCount++;
    }
}
