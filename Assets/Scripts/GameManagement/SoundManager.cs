using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // All sound effects in the game
    // All are public so you can set them in the Inspector
    public AudioClip playerBullet;
    public AudioClip enemyBullet;
    public AudioClip shipExplosion;
    public AudioClip enemyDies;
    public AudioClip enemyNoise1;
    public AudioClip enemyNoise2;

    // Refers to the audio source added to the SoundManager
    // to play sound effects
    private AudioSource source;

    // Holds the single instance of the SoundManager that 
    // you can access from any script
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                _instance.Inited = false;
            }
            return _instance;
        }
    }
    public bool Inited { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // This is a singleton that makes sure you only
        // ever have one Sound Manager
        // If there is any other Sound Manager created destroy it
        if (_instance == null)
        {
            _instance = this;
        } 
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        source = audioSource;
    }

    // Other GameObjects can call this to play sounds
    public void PlayOneShot(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
