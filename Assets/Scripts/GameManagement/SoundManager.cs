using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Add Audio Clips
    public AudioClip PlayerBullet;
    public AudioClip EnemyBullet;
    public AudioClip Explosion;

    private AudioSource source;

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

    public void PlayOneShot(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
