using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Slider slide; 

    public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    public TextMeshProUGUI playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        slide.maxValue = maxHealth;
        slide.value = maxHealth;
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        // The time the Player flashes white when damaged
        if(flashCounter > 0){
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0){
                rend.material.SetColor("_Color", storedColor);
            }
        }
        slide.value = currentHealth;
        playerHealthText.text = currentHealth + "/" + maxHealth;
    }

    // Inflicts damage to Player
    public void HurtPlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.red);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
    }
}
