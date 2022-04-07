using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRenderer : MonoBehaviour
{

    [SerializeField] private KeyboardRowRenderer[] Rows;

    private Dictionary<char, KeyboardSquareRenderer> _keys;
    private Dictionary<char, KeyboardSquareRenderer> Keys
    {
        get
        {
            if (_keys == null)
                _keys = new Dictionary<char, KeyboardSquareRenderer>();
            return _keys;
        }
    }


    public void UpdateUsage(CharacterKnowledge.Knowledge knowledge, char c)
    {
        Keys[c].UpdateColor(knowledge);
    }


    private static readonly string[] KeyboardLayout =
    {
        "qwertyuiop", "asdfghjkl", "zxcvbnm"
    };

    public void ResetGame(Action<char> onPressKey, Action onEnter, Action onDelete)
    {
        for (var row = 0; row < KeyboardLayout.Length; row++)
        {
            if (row == 2)
            {
                Rows[row].AddEnterKey(onEnter);
            }
            for (var col = 0; col < KeyboardLayout[row].Length; col++)
            {
                var c = KeyboardLayout[row].ToUpper()[col];
                if (!Keys.ContainsKey(c))
                    Keys[c] = Rows[row].AddKey(c, () => onPressKey(c));
                Keys[c].Initialize(c, () => onPressKey(c));
            }
            if (row == 2)
            {
                Rows[row].AddDeleteKey(onDelete);
            }
        }
    }

}
