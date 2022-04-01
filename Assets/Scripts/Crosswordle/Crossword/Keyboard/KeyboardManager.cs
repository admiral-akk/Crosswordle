using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<char, KeyboardSquare> _keys;
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private Canvas[] KeyRows;

    private void Awake()
    {
        _keys = new Dictionary<char, KeyboardSquare>();
        for (var i = 0; i < KeyboardLayout.Length; i++)
        {
            foreach (var c in KeyboardLayout[i])
            {
                var key = Instantiate(KeyPrefab, KeyRows[i].transform).GetComponent<KeyboardSquare>();
                key.Letter = c;
                _keys[c] = key;
            }
        }
    }

    private static readonly string[] KeyboardLayout =
    {
        "qwertyuiop", "asdfghjkl", "zxcvbnm"
    };

    public void NewGame()
    {
        foreach (var key in _keys.Values)
        {
            key.NewGame();
        }
    }
}
