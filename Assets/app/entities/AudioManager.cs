using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioS;

    [SerializeField] AudioClip explosionSFX;

    private void Awake()
    {
        if (instance == null) instance = this;

        audioS = GetComponent<AudioSource>();
    }

    public void PlayExplode()
    {
        audioS.PlayOneShot(explosionSFX);
    }
}
