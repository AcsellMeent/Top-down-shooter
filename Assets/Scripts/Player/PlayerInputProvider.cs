using UnityEngine;

public class PlayerInputProvider : MonoBehaviour
{
    private InputActions _input;
    public InputActions Input { get => _input; private set => _input = value; }

    private void Awake()
    {
        _input = new InputActions();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

}
