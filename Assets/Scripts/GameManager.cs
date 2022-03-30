using UnityEngine;
using static Assets.Scripts.Structs.PlayerInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private InputManager input;
    [SerializeField] private AnswerManager answer;
    [SerializeField] private KeyboardManager keyboard;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        answer.NewGame();
        board.NewGame();
        input.NewGame();
        keyboard.NewGame();
    }

    public void GameOver()
    {
        input.GameOver();
    }

    private void Update()
    {
        if (!input.HasInput)
            return;
        var playerInput = input.GetInput();
        switch (playerInput.Type)
        {
            case InputType.None:
                break;
            case InputType.AddLetter:
                board.SubmitLetter(playerInput.Letter);
                break;
            case InputType.Delete:
                board.DeleteLetter();
                break;
            case InputType.SubmitWord:
                board.SubmitWord();
                break;
            case InputType.NewGame:
                NewGame();
                break;
        }
    }
}
