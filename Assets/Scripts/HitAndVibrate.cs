using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



[RequireComponent(typeof(AudioSource))]
public class HitAndVibrate : MonoBehaviour
{
    private AudioSource SoundAudioSource { get; set; }
    [SerializeField]
    private AudioClip _soundClip;
    // Start is called before the first frame update
    void Start()
    {
        SoundAudioSource = GetComponent<AudioSource>();
        SoundAudioSource.clip = _soundClip;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightDrumstick"))
        {
            SoundAudioSource.Play();
            print("Hit");
        }
    }
}
