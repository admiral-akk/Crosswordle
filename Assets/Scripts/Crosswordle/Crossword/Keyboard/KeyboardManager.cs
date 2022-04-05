using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<char, KeyboardSquareRenderer> _keys;
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private Canvas[] KeyRows;

    private void Awake()
    {
        _keys = new Dictionary<char, KeyboardSquareRenderer>();
        for (var i = 0; i < KeyboardLayout.Length; i++)
        {
            foreach (var c in KeyboardLayout[i].ToUpper())
            {
                _keys[c] = Instantiate(KeyPrefab, KeyRows[i].transform).GetComponent<KeyboardSquareRenderer>();
                _keys[c].Initialize(c);
            }
        }
    }

    private static readonly string[] KeyboardLayout =
    {
        "qwertyuiop", "asdfghjkl", "zxcvbnm"
    };

    public void UpdateUsage(CharacterKnowledge hints, Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            _keys[guess[i]].UpdateColor(hints.Get(guess[i]));
        }
    }

    public void ResetGame()
    {
        for (var i = 0; i < KeyboardLayout.Length; i++)
        {
            foreach (var c in KeyboardLayout[i].ToUpper())
            {
                _keys[c].UpdateColor(CharacterKnowledge.Knowledge.None);
            }
        }
    }
}
