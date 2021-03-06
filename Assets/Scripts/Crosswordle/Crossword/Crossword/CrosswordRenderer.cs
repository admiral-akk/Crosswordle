using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswordRenderer : MonoBehaviour
{
    [SerializeField] private GameObject Square;
    [SerializeField] private Bounds Bounds;

    private List<CrosswordSquareRenderer> _squares;
    private CrosswordKnowledge _toRender;

    public void Render(CrosswordKnowledge crossword)
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
        var dimension = new Vector2Int(_toRender.Dimensions.Item1, _toRender.Dimensions.Item2);
        var filledSquares = new HashSet<Vector2Int>();
        foreach (var problem in _toRender.Problems)
        {
            var offset = problem.IsHorizontal ? Vector2Int.right : Vector2Int.up;
            for (var i = 0; i < problem.Length; i++)
            {
                var pos = problem.StartPosition + i * offset;
                if (filledSquares.Contains(pos))
                    continue;
                filledSquares.Add(pos);
                var square = Instantiate(Square, transform).GetComponent<CrosswordSquareRenderer>();
                square.UpdatePosition(problem.StartPosition + i * offset, dimension, Bounds);
                square.UpdateState(problem.GetKnowledge(i));
                _squares.Add(square);
            }
        }
        _toRender = null;
    }
}
