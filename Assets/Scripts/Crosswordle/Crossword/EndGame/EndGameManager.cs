using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Button NewGame;
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private SpriteRenderer Background;

    public void StartGame()
    {
        Background.gameObject.SetActive(false);
        NewGame.onClick.RemoveAllListeners(); 
    }

    public void PlayerLost(UnityEngine.Events.UnityAction newGame)
    {
        Background.gameObject.SetActive(true);
        Text.text = "Game over!";
        NewGame.onClick.AddListener(newGame);
        ButtonText.text = "New game?";
    }

    public void PlayerWon(UnityEngine.Events.UnityAction nextLevel)
    {
        Background.gameObject.SetActive(true);
        Text.text = "You won!";
        NewGame.onClick.AddListener(nextLevel);
        ButtonText.text = "Next level?";
    }
}
