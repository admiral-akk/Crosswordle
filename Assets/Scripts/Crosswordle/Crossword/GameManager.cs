using UnityEngine;
using static Assets.Scripts.Structs.PlayerInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;
    [SerializeField] private CrosswordManager Crossword;
    [SerializeField] private WordTrackerManager WordTracker;
    [SerializeField] private KeyboardManager Keyboard;
    private void Update()
    {
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

    private void Start()
    {
        Guess.UpdateGuessKnowledge(Crossword.GenerateGuessKnowledge());
    }
}
