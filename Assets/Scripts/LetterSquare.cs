using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;

    public char Letter
    {
        get
        {
            if (letter.text == string.Empty)
            {
                return ' ';
            } else
            {
                return letter.text[0];
            }
        }
            set
        {
            letter.text = value.ToString().ToUpper();
        }
    }

    public void ClearLetter()
    {
        letter.text = string.Empty;
    }
}
