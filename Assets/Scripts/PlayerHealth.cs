using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 50;

    private int currHealth;

    private void Awake()
    {
        currHealth = health;
    }

    public void Damage(int amount)
    {
        currHealth -= amount;
        Debug.Log("Player Health: " + currHealth);
        if(currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player Died!");
    }
}
