using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;


    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;
    private int damage = 1;

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obsticle"))
        {
            TakeDamage();
        }



    }


    private void TakeDamage()
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(C_FlashRed());
        if(currentHealth <= 0)
        {
            //playerdead
        }
    }

    private IEnumerator C_FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

}
