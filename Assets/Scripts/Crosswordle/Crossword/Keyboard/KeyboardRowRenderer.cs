using System;
using UnityEngine;

public class KeyboardRowRenderer : MonoBehaviour
{
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private GameObject CommandKeyPrefab;

    private bool _hasEnter;
    private bool _hasDelete;

    public KeyboardSquareRenderer AddKey(char c, Action onClick)
    {
        var key = Instantiate(KeyPrefab, transform).GetComponent<KeyboardSquareRenderer>();
        key.Initialize(c, () => onClick());
        return key;
    }

    public void AddEnterKey(Action onEnter)
    {
        if (_hasEnter)
            return;
        _hasEnter = true;
        var key = Instantiate(CommandKeyPrefab, transform).GetComponent<KeyCommandSquareRenderer>();
        key.Initialize("ENTER", () => onEnter());
    }
    public void AddDeleteKey(Action onDelete)
    {
        if (_hasDelete)
            return;
        _hasDelete = true;
        var key = Instantiate(CommandKeyPrefab, transform).GetComponent<KeyCommandSquareRenderer>();
        key.Initialize("DEL", () => onDelete());
    }
}
