using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;

namespace Services {

    /*
        Enum for Sound Types.
    */
    public enum SoundType {
        BUTTON_CLICK,
        CHEST_OPENED
    }

    /*
        Serializable Class SoundInfo to maintain different properties.
    */
    [System.Serializable]
    public class SoundInfo {

        public SoundType soundType;
        public AudioClip clip;
        public bool loop;

        [HideInInspector]
        public AudioSource audioSource;

        [Range(0, 1)]
        public float volume;
        
    }

    /*
        AudioService MonoSingleton class. Handles all the Audio in the Project.
    */
    public class AudioService : GenericMonoSingleton<AudioService>
    {
        public SoundInfo[] Sounds;

        protected override void Awake() {
            base.Awake();
            for (int i = 0; i < Sounds.Length; i++) {
                Sounds[i].audioSource = gameObject.AddComponent<AudioSource>();
                Sounds[i].audioSource.loop = Sounds[i].loop;
                Sounds[i].audioSource.volume = Sounds[i].volume;
                Sounds[i].audioSource.clip = Sounds[i].clip;
            }
        }

        /*
            Plays the Audio of specified SoundType.
        */
        public void PlayAudio(SoundType soundType) {
            SoundInfo soundInfo = Array.Find(Sounds, item => item.soundType == soundType);
            soundInfo.audioSource.Play();
        }

        /*
            Stops the Audio of specified SoundType.
        */
        public void StopAudio(SoundType soundType) {
            SoundInfo soundInfo = Array.Find(Sounds, item => item.soundType == soundType);
            soundInfo.audioSource.Stop();
        }


    }

}