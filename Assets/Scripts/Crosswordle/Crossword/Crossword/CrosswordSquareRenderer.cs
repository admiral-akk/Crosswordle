using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CrosswordSquareRenderer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Color None;
    [SerializeField] private Color Wrong;
    [SerializeField] private Color WrongPosition;
    [SerializeField] private Color Correct;
    [Header("Components")]
    [SerializeField] private SpriteRenderer Background;
    [SerializeField] private TextMeshProUGUI Letter;
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

    private enum GuessState
    {
        None,
        WrongPosition,
        Correct
    }
    private GuessState _state;

    private GuessState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            switch (_state)
            {
                case GuessState.None:
                    Background.color = None;
                    break;
                case GuessState.WrongPosition:
                    Background.color = WrongPosition;
                    break;
                case GuessState.Correct:
                    Background.color = Correct;
                    break;
            }
        }
    }

    private void Awake()
    {
        State = GuessState.None;
        _hints = new List<HintSquareRenderer>();
    }

    public void UpdateState(LetterKnowledgeState knowledge)
    {
        foreach (var hint in _hints)
            Destroy(hint.gameObject);
        _hints.Clear();
        if (knowledge.IsSolved)
        {
            Letter.text = knowledge.Answer.ToString();
            State = GuessState.Correct;
        }
        if (State == GuessState.Correct) { 
            return;
        } 
        if (knowledge.Hints.Count > 0)
        {
            foreach(var hint in knowledge.Hints)
            {
                var hintObject = Instantiate(HintPrefab, transform).GetComponent<HintSquareRenderer>();
                hintObject.UpdateLetter(hint);
                hintObject.UpdatePosition(_hints.Count);
                _hints.Add(hintObject);
            }
        }
        Letter.text = "";
        State = GuessState.None;
    }
}
