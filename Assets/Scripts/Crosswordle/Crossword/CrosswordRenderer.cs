using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordRenderer : MonoBehaviour
{
    [SerializeField] private GameObject Square;

    private List<CrosswordSquareRenderer> _squares;
    private CrosswordData? _toRender;

    public void Render(CrosswordData crossword)
    {
        _toRender = crossword;
    }

    private void Update()
    {
        if (_toRender == null)
            return;
        if (_squares == null)
            _squares = new List<CrosswordSquareRenderer>();
        foreach (var square in _squares)
            Destroy(square.gameObject);
        _squares.Clear();
        var dimension = new Vector2Int(_toRender.Value.xDim, _toRender.Value.yDim);
        foreach (var word in _toRender.Value.Words)
        {
            var offset = word.IsHorizontal ? Vector2Int.right : Vector2Int.up;
            for (var i = 0; i < word.Word.Length; i++)
            {
                var square = Instantiate(Square, transform).GetComponent<CrosswordSquareRenderer>();
                square.UpdatePosition(word.StartPosition + i * offset, dimension);
                square.UpdateLetter(word.Word[i].ToString());
                _squares.Add(square);
            }
        }
        _toRender = null;
    }
}
