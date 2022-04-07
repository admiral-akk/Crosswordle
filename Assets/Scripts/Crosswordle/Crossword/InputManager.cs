using Assets.Scripts.Structs;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput? _input;
    public bool HasInput => _input.HasValue;
    public PlayerInput GetInput()
    {
        var input = _input.Value;
        _input = null;
        return input;
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
            return;
        var c = (char)e.keyCode;
        if (char.IsLetter(c))
        {
            OnKeyClick(c);
            return;
        }
        if (e.keyCode == KeyCode.Return)
        {
            OnEnterClick();
            return;
        }
        if (e.keyCode == KeyCode.Backspace)
        {
            OnDeleteClick();
            return;
        }
    }

    public void OnKeyClick(char c)
    {
        _input = PlayerInput.AddLetter(c);
    }

    public void OnEnterClick()
    {
        _input = PlayerInput.Enter();
    }
    public void OnDeleteClick()
    {
        _input = PlayerInput.Delete();
    }
}
