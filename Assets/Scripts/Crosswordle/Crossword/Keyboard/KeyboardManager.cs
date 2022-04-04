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

    public void UpdateUsage(CharacterKnowledge hints, Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            switch (hints.Get(guess[i]))
            {
                case CharacterKnowledge.Knowledge.None:
                    _keys[guess[i]].S = KeyboardSquare.State.None;
                    break;
                case CharacterKnowledge.Knowledge.NotInCrossword:
                    _keys[guess[i]].S = KeyboardSquare.State.Wrong;
                    break;
                case CharacterKnowledge.Knowledge.Incomplete:
                    _keys[guess[i]].S = KeyboardSquare.State.WrongPosition;
                    break;
                case CharacterKnowledge.Knowledge.Complete:
                    _keys[guess[i]].S = KeyboardSquare.State.RightPosition;
                    break;
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
