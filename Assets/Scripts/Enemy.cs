using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath;

    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float damageCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private float nextDamageTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!player) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void Die()
    {
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Time.time >= nextDamageTime)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                if(playerHealth)
                {
                    playerHealth.Damage(10);
                    nextDamageTime = Time.time + damageCooldown;
                }            
            }
        }
    }
}
