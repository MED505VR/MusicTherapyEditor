using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



[RequireComponent(typeof(AudioSource))]
public class HitAndVibrate : MonoBehaviour
{
    private AudioSource SoundAudioSource { get; set; }
    [SerializeField]
    private AudioClip _soundClip;

    //[SerializeField] private float amplitude;
    //[SerializeField] private float duration;

    //private XRController _xr;
    
    // Start is called before the first frame update
    void Start()
    {
        SoundAudioSource = GetComponent<AudioSource>();
        SoundAudioSource.clip = _soundClip;
        //_xr = GetComponent<XRController>();
    }
    //void ActivateHaptic()
    //{
        //_xr.SendHapticImpulse(0.7f, 2f);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightDrumstick"))
        {
            SoundAudioSource.Play();
            print("Hit");
            //ActivateHaptic();
        }
    }
}
