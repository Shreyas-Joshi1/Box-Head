using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private Vector2 inputVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        inputVec = inputHandler.InputActions.Player.Move.ReadValue<Vector2>();
    }

    //Normalize input vector to prevent faster diagonal movement
    private void FixedUpdate()
    {
        rb.linearVelocity = inputVec.normalized * moveSpeed;
    }
}
