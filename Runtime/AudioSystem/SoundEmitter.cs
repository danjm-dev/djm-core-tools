using UnityEngine;
using UnityEngine.Audio;

namespace DJM.CoreTools.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundEmitter : MonoBehaviour
    {
        public AudioSource Source { get; private set; }

        public bool IsPlaying => Source.isPlaying;
        
        private void Awake()
        {
            Source = GetComponent<AudioSource>();
            Source.playOnAwake = false;
            Source.loop = false;
        }

        public void Initialize
        (
            AudioClip audioClip, 
            AudioMixerGroup mixerGroup, 
            Vector3 position, 
            float volume, 
            float pitch
        )
        {
            Source.clip = audioClip;
            Source.outputAudioMixerGroup = mixerGroup;
            transform.position = position;
            Source.volume = volume;
            Source.pitch = pitch;
        }
        
        public void Clear()
        {
            Source.clip = null;
            Source.outputAudioMixerGroup = null;
            transform.position = Vector3.zero;
            Source.volume = 1f;
            Source.pitch = 1f;
        }

        public void Play() => Source.Play();
        public void Play(float delay) => Source.PlayDelayed(delay);
        public void Pause() => Source.Pause();
        public void UnPause() => Source.UnPause();
        public void Stop() => Source.Stop();
    }
}