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
            _input = PlayerInput.AddLetter(c);
            return;
        }
        if (e.keyCode == KeyCode.Return)
        {
            _input = PlayerInput.Enter();
            return;
        }
        if (e.keyCode == KeyCode.Backspace)
        {
            _input = PlayerInput.Delete();
            return;
        }
    }
}
