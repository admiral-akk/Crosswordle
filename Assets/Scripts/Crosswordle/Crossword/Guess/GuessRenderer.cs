using System.Collections.Generic;
using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private GameObject SquarePrefab;
    [SerializeField] private Bounds Bounds;

    private List<GuessSquareRenderer> _squares;
    private List<GuessSquareRenderer> Squares
    {
        get
        {
            if (_squares == null)
                _squares = new List<GuessSquareRenderer>();
            return _squares;
        }
    }
    private Word? _toRender;
    private GuessKnowledge _knowledge;
    private int _wordLength;

    private void InitializeSquares()
    {
        foreach (var square in Squares)
        {
            Destroy(square);
        }
        Squares.Clear();
        for (var i = 0; i < _wordLength; i++)
        {
            var square = Instantiate(SquarePrefab, transform).GetComponent<GuessSquareRenderer>();
            square.UpdatePosition(new Vector2Int(i, 0), new Vector2Int(_wordLength, 1), Bounds);
            square.UpdateLetter(' ');
            Squares.Add(square);
        }
    }

    private void Update()
    {
        if (_toRender == null)
            return;
        var word = _toRender.Value;
        _toRender = null;
        if (Squares.Count != _wordLength)
        {
            InitializeSquares();
        }
        foreach (var square in Squares)
        {
            square.UpdateLetter(' ');
        }
        for (var i = 0; i < word.Length; i++)
        {
            Squares[i].UpdateLetter(word[i], _knowledge.Get(word[i], i));
        }
        _knowledge = null;
    }

    public void Render(Word word, GuessKnowledge knowledge, int wordLength)
    {
        _toRender = word;
        _knowledge = knowledge;
        _wordLength = wordLength;
        gameObject.SetActive(true);
    }
}
