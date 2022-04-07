using UnityEngine;
using static Assets.Scripts.Structs.PlayerInput;

public class GameManager : MonoBehaviour
{
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
        switch (S)
        {
            default: 
                break;
            case State.None:
            case State.Loading:
            case State.Win:
            case State.Loss:
                return;
        }
        if (Explainer.IsOpen)
            return;
        if (Crossword.PlayerWon)
        {
            S = State.Win;
            return;
        }
        if (WordTracker.PlayerLost)
        {
            S = State.Loss;
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

    private enum State
    {
        None,
        Loading,
        Playing,
        Win,
        Loss
    }

    private State _s;
    private State S
    {
        get => _s;
        set
        {
            _s = value;
            switch (_s)
            {
                case State.None:
                    break;
                case State.Loading:
                    break;
                case State.Playing:
                    break;
                case State.Win:
                    EndGame.PlayerWon(NextLevel);
                    Guess.GameOver();
                    break;
                case State.Loss:
                    Crossword.SpoilCrossword();
                    EndGame.PlayerLost(NewGame);
                    Guess.GameOver();
                    break;
            }
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
        S = State.Loading;
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
        S = State.Playing;
    }

    private void DictionaryLoaded()
    {
        Crossword.Initialize(Dictionary);
        Guess.Initialize(Dictionary);
        NewGame();
    }

    private void Start()
    {
        Initialize();
    }
}
