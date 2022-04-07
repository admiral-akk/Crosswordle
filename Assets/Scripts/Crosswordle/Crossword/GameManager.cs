using UnityEngine;
using static Assets.Scripts.Structs.PlayerInput;

public class GameManager : MonoBehaviour
{
    private bool _isReady;
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;
    [SerializeField] private CrosswordManager Crossword;
    [SerializeField] private WordTrackerManager WordTracker;
    [SerializeField] private KeyboardManager Keyboard;
    [SerializeField] private EndGameManager EndGame;
    [SerializeField] private DictionaryManager Dictionary;
    [SerializeField] private ExplainerManager Explainer;
    [SerializeField] private DifficultyManager Difficulty;

    private void Update()
    {
        if (!_isReady)
            return;
        if (Explainer.IsOpen)
            return;
        if (Crossword.PlayerWon)
        {
            EndGame.PlayerWon(NextLevel);
            Guess.GameOver();
            return;
        }
        if (WordTracker.PlayerLost)
        {
            Crossword.SpoilCrossword();
            EndGame.PlayerLost(() => { });
            Guess.GameOver();
            return;
        }
        if (!Input.HasInput)
            return;
        var playerInput = Input.GetInput();
        switch (playerInput.Type)
        {
             default:
                break;
            case InputType.AddLetter:
                Guess.SubmitLetter(playerInput.Letter);
                break;
            case InputType.Delete:
                Guess.DeleteLetter();
                break;
            case InputType.SubmitWord:
                var word = Guess.SubmitWord();
                if (!word.HasValue)
                    return;
                Crossword.HandleGuess(word.Value);
                WordTracker.AddWord(word.Value);
                var hints = Crossword.GetGlobalLetterKnowledge();
                Keyboard.UpdateUsage(hints, word.Value);
                Guess.UpdateGuessKnowledge(Crossword.GenerateGuessKnowledge());
                break;
        }
    }

    public void NextLevel()
    {
        Difficulty.OnWin();
        NewPuzzle();
    }

    public void NewGame()
    {
        Difficulty.FreshRun();
        NewPuzzle();
    }

    public void Initialize()
    {
        Explainer.ShowHelp();
        Dictionary.Initialize(DictionaryLoaded);
    }

    public void NewPuzzle()
    {
        Crossword.GenerateCrossword(Difficulty.WordCount, Difficulty.WordLength);
        WordTracker.SetGuessCount(Difficulty.GuessCount);
        Keyboard.ClearKnowledge();
        EndGame.StartGame();
        Guess.SetWordLength(Difficulty.WordLength);
        Guess.UpdateGuessKnowledge(Crossword.GenerateGuessKnowledge());
    }

    private void DictionaryLoaded()
    {
        Crossword.Initialize(Dictionary);
        Guess.Initialize(Dictionary);
        NewGame();
        _isReady = true;
    }

    private void Start()
    {
        Initialize();
    }
}
