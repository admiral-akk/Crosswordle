using System;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] private KeyboardRenderer Renderer;

    public void UpdateUsage(CharacterKnowledge hints, Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            Renderer.UpdateUsage(hints.Get(guess[i]), guess[i]);
        }
    }

    public void ResetGame(Action<char> onKeyClick, Action onEnter, Action onDelete)
    {
        Renderer.ResetGame(onKeyClick,  onEnter,  onDelete);
    }
}
