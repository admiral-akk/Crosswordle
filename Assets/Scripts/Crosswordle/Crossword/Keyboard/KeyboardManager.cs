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
        _usedLetters = new HashSet<char>();
        _guess = new List<Word>();
        _keys = new Dictionary<char, KeyboardSquare>();
        for (var i = 0; i < KeyboardLayout.Length; i++)
        {
            foreach (var c in KeyboardLayout[i].ToUpper())
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

    private HashSet<char> _usedLetters;
    private List<Word> _guess;

    public void UpdateUsage(KeyboardHints hints, Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            _usedLetters.Add(c);
            if (hints.Hints.ContainsKey(c))
            {
                switch (hints.Hints[c])
                {
                    default:
                        _keys[guess[i]].S = KeyboardSquare.State.Wrong;
                        break;
                    case KeyboardHints.State.OnBoard:
                        _keys[guess[i]].S = KeyboardSquare.State.WrongPosition;
                        break;
                    case KeyboardHints.State.Complete:
                        _keys[guess[i]].S = KeyboardSquare.State.RightPosition;
                        break;
                }
            } else
            {
                _keys[guess[i]].S = KeyboardSquare.State.Wrong;
            }
        }
    }

    public void NewGame()
    {
        foreach (var key in _keys.Values)
        {
            key.NewGame();
        }
    }
}
