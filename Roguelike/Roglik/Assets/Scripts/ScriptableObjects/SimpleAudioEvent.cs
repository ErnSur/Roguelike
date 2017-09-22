using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Audio Evants/Simple")]
public class SimpleAudioEvent : AudioEvent {

    public AudioClip[] clips;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0) return;

        source.Play();
       
    }
}
