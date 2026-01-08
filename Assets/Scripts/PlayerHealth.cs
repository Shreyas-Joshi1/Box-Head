using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Slider healthSlider;

    private int currHealth;

    private void Awake()
    {
        currHealth = health;

        healthSlider.maxValue = health;
        healthSlider.value = currHealth;
    }

    public void Damage(int amount)
    {
        currHealth -= amount;
        healthSlider.value = currHealth;
        Debug.Log("Player Health: " + currHealth);
        if(currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
}
