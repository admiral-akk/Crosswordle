using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordRenderer : MonoBehaviour
{
    [SerializeField] private GameObject Square;

    private List<GameObject> _squares;
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
            _squares = new List<GameObject>();
        foreach (var square in _squares)
            Destroy(square);
        foreach (var word in _toRender.Value.Words)
        {
            for (var i = 0; i < word.Word.Length; i++)
            {
                var square = Instantiate(Square, transform);
                var position = new Vector3(word.StartPosition.x, word.StartPosition.y, 0);
                var offset = word.IsHorizontal ? Vector3.right : Vector3.up;
                square.transform.localPosition = position + i * offset;
                _squares.Add(square);
            }
        }
        _toRender = null;
    }
}
