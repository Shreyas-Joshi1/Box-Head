using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath;

    [SerializeField] private float moveSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;

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
}
