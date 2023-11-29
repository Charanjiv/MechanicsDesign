using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] int maxHealth = 3;
    private int currentHealth;
    [SerializeField] float respawnDuration;
    [SerializeField] private Rigidbody2D m_RB;
    [SerializeField] private float IFramesDur;
    [SerializeField] private int IFramesFlash;
    

    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;
    private int damage = 1;
    private Vector2 checkPoint;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        m_RB = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        checkPoint = transform.position;
        SetHealth();

        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obsticle"))
        {
            TakeDamage();
        }



    }

    private void SetHealth()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    private void TakeDamage()
    {
        audioManager.PlayerSFX(audioManager.Damage);
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(C_FlashRed());
        //StartCoroutine(C_InvincibilityFrame());
        if(currentHealth <= 0)
        {
            audioManager.PlayerSFX(audioManager.Death);
            StartCoroutine(C_Respawn(respawnDuration));
            
        }
    }


 
    private IEnumerator C_Respawn(float duration)
    {
        m_RB.simulated = false;
        m_RB.velocity = new Vector2(0, 0);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = checkPoint;
        spriteRenderer.enabled = true;
        m_RB.simulated = true;
        SetHealth();
    }

    private IEnumerator C_FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public void UpdateCkeckpoint(Vector2 pos)
    {
        checkPoint = pos;
    }

    /*private IEnumerator C_InvincibilityFrame()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        
            spriteRenderer.color = Color.grey;
            yield return new WaitForSeconds(1);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(1);
        
        Physics2D.IgnoreLayerCollision(6, 7, false);

    }*/

}
