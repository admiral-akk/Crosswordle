using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;

    public void SetLetter(char l)
    {
        letter.text = l.ToString().ToUpper();
    }

    public void ClearLetter()
    {
        letter.text = string.Empty;
    }
}
