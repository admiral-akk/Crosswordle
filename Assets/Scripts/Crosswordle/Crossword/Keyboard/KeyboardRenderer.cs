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

    public void ResetGame()
    {
        for (var row = 0; row < KeyboardLayout.Length; row++)
        {
            foreach (var c in KeyboardLayout[row].ToUpper())
            {
                if (!Keys.ContainsKey(c))
                    Keys[c] = Rows[row].AddKey(c);
                Keys[c].Initialize(c);
            }
        }
    }

}
