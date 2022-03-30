using Assets.Scripts.Structs;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<char, KeySquare> _keys;
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private Canvas[] KeyRows;

    private void Awake()
    {
        _keys = new Dictionary<char, KeySquare>();
        for (var i = 0; i < KeyboardLayout.Length; i++)
        {
            foreach (var c in KeyboardLayout[i])
            {
                var key = Instantiate(KeyPrefab, KeyRows[i].transform).GetComponent<KeySquare>();
                key.Letter = c;
                _keys[c] = key;
            }
        }
    }

    private static readonly string[] KeyboardLayout =
    {
        "qwertyuiop", "asdfghjkl", "zxcvbnm"
    };

    public void HandleResult(GuessResult result)
    {
        for (var i = 0; i < result.Guess.Length;i++)
        {
            var c = result.Guess[i];
            var resultVal = result.Results[i];
            _keys[c].HandleResult(resultVal);
            Debug.Log("Char '" + c + "' is now '" + resultVal.ToString() + "'");
        }
    }
}
