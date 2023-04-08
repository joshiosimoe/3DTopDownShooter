using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    private int currentHealth;
    public int money;

    [SerializeField] private EnemyHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.UpdateHealthBar(health, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys Enemy once health reaches 0
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            ShopManager.totalMoney += money;
        }
    }

    // Inflicts damage to enemy
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(health, currentHealth);
    }

}
