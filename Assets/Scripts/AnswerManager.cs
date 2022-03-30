using Assets.Scripts.Structs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject image;
    [SerializeField] private Color Correct;
    [SerializeField] private Color Wrong;

    private Image Img => image.GetComponent<Image>();
    public void NewGame()
    {
        image.SetActive(false);
    }

    public void GameOver(Guess answer)
    {
        image.SetActive(true);
        text.text = answer.ToString().ToUpper();
        Img.color = Wrong;
    }

    public void Success(Guess answer)
    {
        image.SetActive(true);
        text.text = answer.ToString().ToUpper();
        Img.color = Correct;
    }

}
