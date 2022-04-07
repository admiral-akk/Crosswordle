
using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyCommandSquareRenderer : MonoBehaviour
{
    [SerializeField] private CommandSquareRenderer CommandSquare;
    [SerializeField] private Button KeyClick;

    public void Initialize(string command, Action onClick)
    {
        CommandSquare.SetCommand(command);
        KeyClick.onClick.AddListener(() => onClick());
    }
}
