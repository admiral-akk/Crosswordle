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
    [SerializeField] private GameOverManager GameOver;
    [SerializeField] private DictionaryManager Dictionary;

    private void Update()
    {
        if (!_isReady)
            return;
        if (Crossword.PlayerWon)
        {
            GameOver.GameOver(true);
            Guess.GameOver();
            return;
        }
        if (WordTracker.PlayerLost)
        {
            Crossword.SpoilCrossword();
            GameOver.GameOver(false);
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

    public void ResetGame()
    {
        Guess.ResetGame();
        Crossword.ResetGame();
        WordTracker.ResetGame();
        Keyboard.ResetGame();
        GameOver.ResetGame();
        Guess.UpdateGuessKnowledge(Crossword.GenerateGuessKnowledge());
    }

    private void DictionaryLoaded()
    {
        Crossword.Initialize(Dictionary);
        Guess.Initialize(Dictionary);
        ResetGame();
        _isReady = true;
    }

    private void Start()
    {
        Dictionary.Initialize(DictionaryLoaded);
        GameOver.RegisterReset(ResetGame);
    }
}
