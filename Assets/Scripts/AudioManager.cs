using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Type of Audio")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip geiger;

    void Start()
    {
        music.clip = background;
        SFX.clip = geiger;
        music.Play();
        SFX.Play();
    }

    void Update()
    {
        
    }
}
