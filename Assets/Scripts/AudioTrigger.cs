using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] string triggerTag = "Player";
    [SerializeField] AudioClip audioToPlay;
    [SerializeField] float volume = 1f;
    [SerializeField] float pitch = 1f;

    [SerializeField] AudioPlayer audioPlayerPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == triggerTag)
        {
            PlayAudio();
        }
    }

    private void PlayAudio()
    {
        AudioPlayer newPlayer = Instantiate(audioPlayerPrefab);
        newPlayer.PlayAudio(audioToPlay,volume,pitch);
    }
}

