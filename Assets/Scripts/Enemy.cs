using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath;

    [SerializeField] private float damageCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private float nextDamageTime = 0f;
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void FixedUpdate()
    {
        if (!player) return;

        agent.SetDestination(player.position);
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
