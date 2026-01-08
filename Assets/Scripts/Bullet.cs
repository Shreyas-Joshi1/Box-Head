using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float lifetime = 2f;

    private Rigidbody2D rb;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * moveSpeed;
        Destroy(gameObject, lifetime);
    }

    // Handle collision with enemy or obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasHit) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                hasHit = true;
                enemy.Die();
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
