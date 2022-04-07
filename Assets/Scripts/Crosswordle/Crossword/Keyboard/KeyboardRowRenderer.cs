using System;
using UnityEngine;

public class KeyboardRowRenderer : MonoBehaviour
{
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private GameObject CommandKeyPrefab;

    public KeyboardSquareRenderer AddKey(char c, Action onClick)
    {
        var key = Instantiate(KeyPrefab, transform).GetComponent<KeyboardSquareRenderer>();
        key.Initialize(c, () => onClick());
        return key;
    }

    public void AddEnterKey(Action onEnter)
    {
        var key = Instantiate(CommandKeyPrefab, transform).GetComponent<KeyboardSquareRenderer>();
        key.Initialize('1', () => onEnter());
    }
    public void AddDeleteKey(Action onDelete)
    {
        var key = Instantiate(CommandKeyPrefab, transform).GetComponent<KeyboardSquareRenderer>();
        key.Initialize('2', () => onDelete());
    }
}
