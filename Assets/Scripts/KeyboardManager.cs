using Assets.Scripts.Structs;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Structs.GuessResult;

public class KeyboardManager : MonoBehaviour
{
    private Dictionary<char, ResultType> _keys;

    private void Awake()
    {
        _keys = new Dictionary<char, ResultType>();
    }

    public void HandleResult(GuessResult result)
    {
        for (var i = 0; i < result.Guess.Length;i++)
        {
            var c = result.Guess[i];
            var resultVal = result.Results[i];
            _keys[c] = resultVal;
            Debug.Log("Char '" + c + "' is now '" + resultVal.ToString() + "'");
        }
    }
}
