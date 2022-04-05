using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CrosswordSquareRenderer : MonoBehaviour
{
    [Header("Letter Square")]
    [SerializeField] private LetterSquareRenderer LetterSquare;
    [Header("Prefabs")]
    [SerializeField] private GameObject HintPrefab;

    private List<HintSquareRenderer> _hints;

    private static float Size(Vector2Int dimensions, Bounds bounds)
    {
        var size = Mathf.Min(bounds.size.x / dimensions.x, bounds.size.y / dimensions.y);
        return Mathf.Min(0.75f, size);
    }
    public void UpdatePosition(Vector2Int position, Vector2Int dimensions, Bounds bounds)
    {
        var size = Size(dimensions, bounds);
        transform.localScale = size * Vector3.one;
        transform.localPosition = new Vector3(position.x - dimensions.x / 2f, dimensions.y / 2f - position.y) * size;
        if (dimensions.x / 2f < Mathf.Abs(transform.localPosition.x )|| dimensions.y / 2f < Mathf.Abs(transform.localPosition.y))
        {
            Debug.Log("ERROR");
        }
    }

    private void Awake()
    {
        _hints = new List<HintSquareRenderer>();
    }

    private void ClearHints()
    {
        foreach (var hint in _hints)
            Destroy(hint.gameObject);
        _hints.Clear();
    }

    private void AddHints(HashSet<char> hints)
    {
        foreach (var hint in hints)
        {
            var hintObject = Instantiate(HintPrefab, transform).GetComponent<HintSquareRenderer>();
            hintObject.UpdateLetter(hint);
            hintObject.UpdatePosition(_hints.Count);
            _hints.Add(hintObject);
        }
    }

    public void UpdateState(LetterKnowledgeState knowledge)
    {
        ClearHints();
        if (knowledge.IsSolved)
        {
            LetterSquare.Render(knowledge.Answer.Value, LetterSquareRenderer.State.Correct);
            return;
        }
        LetterSquare.Render(' ', LetterSquareRenderer.State.Empty);
        AddHints(knowledge.Hints);
    }
}
