using System;
using UnityEngine;
using UnityEngine.Audio;

namespace DJM.CoreTools.AudioSystem
{
    [Serializable]
    public sealed class SoundData
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private bool loop;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool frequentSound;
        
        public AudioClip Clip => clip;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public bool Loop => loop;
        public bool PlayOnAwake => playOnAwake;
        public bool FrequentSound => frequentSound;
    }
}