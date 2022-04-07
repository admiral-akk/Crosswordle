using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExplainerManager : MonoBehaviour
{
    [SerializeField] private Canvas ExplainerCanvas;
    [SerializeField] private Button ExplainerButton;
    [SerializeField, Range(0,3)] private float RecentlyOpenDuration;

    private enum State
    {
        None,
        RecentlyOpened,
        Open,
        Closed,
    }

    public bool IsOpen => S == State.RecentlyOpened || S == State.Open;

    private void Awake()
    {
        ExplainerButton.onClick.AddListener(ShowHelp);
    }

    private IEnumerator EndRecentlyOpen()
    {
        yield return new WaitForSeconds(RecentlyOpenDuration); 
        S = State.Open; 
    }

    private State _s;
    private State S
    {
        get => _s;
        set
        {
            switch (_s)
            {
                default:
                    if (value == State.RecentlyOpened)
                    {
                        _s = value;
                        StartCoroutine(EndRecentlyOpen());
                        ExplainerCanvas.gameObject.SetActive(true);
                    }
                    break;
                case State.RecentlyOpened:
                    if (value == State.Open)
                    {
                        _s = value;
                    }
                    break;
                case State.Open:
                    if (value == State.Closed)
                    {
                        _s = value;
                        ExplainerCanvas.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public void ShowHelp()
    {
        S = State.RecentlyOpened;
    }

    void OnGUI()
    {
        if (S != State.Open)
            return;
        Event e = Event.current;
        if (e.type != EventType.MouseDown && e.type != EventType.KeyDown)
            return;
        S = State.Closed;
    }
}
