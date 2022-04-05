using System.Collections.Generic;
using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private GameObject Square;
    [SerializeField] private Bounds Bounds;

    private List<GuessSquareRenderer> _squares;
    private Word? _toRender;
    private GuessKnowledge _knowledge;

    private void Awake()
    {
        _squares = new List<GuessSquareRenderer>();
        for (var i = 0; i < 5; i++)
        {
            var square = Instantiate(Square, transform).GetComponent<GuessSquareRenderer>();
            square.UpdatePosition(new Vector2Int(i, 0), new Vector2Int(5, 1), Bounds);
            square.UpdateLetter(' ');
            _squares.Add(square);
        }
    }

    private void Update()
    {
        if (_toRender == null)
            return;
        var word = _toRender.Value;
        _toRender = null;
        foreach (var square in _squares)
        {
            square.UpdateLetter(' ');
        }
        for (var i = 0; i < word.Length; i++)
        {
            _squares[i].UpdateLetter(word[i], _knowledge.Get(word[i], i));
        }
        _knowledge = null;
    }

    public void Render(Word word, GuessKnowledge knowledge)
    {
        _toRender = word;
        _knowledge = knowledge;
    }
}
