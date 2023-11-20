using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] int maxHealth = 3;
    private int currentHealth;


    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;
    private int damage = 1;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
        audioManager.PlayerSFX(audioManager.Damage);
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(C_FlashRed());
        if(currentHealth <= 0)
        {
            //playerdead
            audioManager.PlayerSFX(audioManager.Damage);
        }
    }

    private IEnumerator C_FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

}
