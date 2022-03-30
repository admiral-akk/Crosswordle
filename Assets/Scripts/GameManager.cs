using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public static void WordSubmitted()
    {
        BoardManager.SubmitWord();
    }

    public static void LetterEntered(char letter)
    {
        BoardManager.SubmitLetter(letter);
    }
    public static void DeleteLetter()
    {
        BoardManager.DeleteLetter();
    }
}
