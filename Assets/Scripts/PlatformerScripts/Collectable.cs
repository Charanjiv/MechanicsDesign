using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IItem
{
    public static event Action<int> OnCollect;
    [SerializeField] private int worth = 5;
    private AudioManager audioManager;


    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Collect()
    {
        OnCollect.Invoke(worth);
        Destroy(gameObject);
        audioManager.PlayerSFX(audioManager.collectablePickUp);
    }


}
