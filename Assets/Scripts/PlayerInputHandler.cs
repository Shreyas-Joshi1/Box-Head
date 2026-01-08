using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public InputSystem_Actions InputActions {get; private set;}

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
    }
}
