using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Button NewGame;
    [SerializeField] private SpriteRenderer Background;

    public void ResetGame()
    {
        Background.gameObject.SetActive(false);
    }

    public void GameOver(bool playerWon)
    {
        Background.gameObject.SetActive(true);
        if (playerWon)
            Text.text = "You win!";
        else
            Text.text = "You lose.";
    }

    public void RegisterReset(UnityEngine.Events.UnityAction action)
    {
        NewGame.onClick.AddListener(action);
    }

    private void Awake()
    {
        NewGame.onClick.AddListener(ResetGame);
    }
}
