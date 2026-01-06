using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    private Vector2 inputVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        inputVec = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputVec.normalized * moveSpeed;
        Debug.Log(rb.linearVelocity);
    }
}
