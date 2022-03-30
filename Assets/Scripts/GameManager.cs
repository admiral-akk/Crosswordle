using UnityEngine;
using static Assets.Scripts.Structs.PlayerInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private InputManager input;

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
                Debug.Log("hit enter");
                board.SubmitWord();
                break;
        }
    }
}
