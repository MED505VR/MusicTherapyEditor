using System.Collections;
using UnityEngine;

public class EmojiSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundAudioSource;
    [SerializeField] private AudioClip audioClip;

    private bool _recentlyTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightDrumstick"))
        {
            if (_recentlyTriggered) return;
            var isPlaying = soundAudioSource.isPlaying;
            if (isPlaying || soundAudioSource.clip == audioClip)
            {
                soundAudioSource.Stop();
                soundAudioSource.clip = null;
            }
            else
            {
                soundAudioSource.Stop();
                soundAudioSource.clip = audioClip;
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