using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSound : MonoBehaviour
{
    private AudioSource soundAudioSource;
    private GameObject wall;
    
    private bool _recentlyTriggered;

    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        wall = GameObject.Find("Emoji");
        soundAudioSource = wall.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightDrumstick"))
        {
            if (_recentlyTriggered) return;
            var isPlaying = soundAudioSource.isPlaying;
            if (isPlaying || soundAudioSource.clip == _audioClip)
            {
                soundAudioSource.Stop();
                soundAudioSource.clip = null;
            }
            else
            {
                soundAudioSource.Stop();
                soundAudioSource.clip = _audioClip;
                soundAudioSource.Play();
            }
            StartCoroutine(RecentlyTriggeredWait());
            
        }
    }


    private IEnumerator RecentlyTriggeredWait()
    {
        _recentlyTriggered = true;
        yield return new WaitForSeconds(0.2f);
        _recentlyTriggered = false;
    }
    
}
