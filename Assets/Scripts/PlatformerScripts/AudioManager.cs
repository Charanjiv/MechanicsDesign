using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;

    public AudioClip Jump;
    public AudioClip Damage;
    public AudioClip collectablePickUp;
    public AudioClip Death;
    public AudioClip Checkpoint;
    public AudioClip ButtonPress;
    public AudioClip Dash;

    public static AudioManager  Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayerSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ButtonPressed()
    {
        PlayerSFX(Checkpoint);
    }

}
