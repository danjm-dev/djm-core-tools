using UnityEngine;
using UnityEngine.Audio;

namespace DJM.CoreTools.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundEmitter : MonoBehaviour
    {
        private AudioSource _source;

        public bool IsPlaying => _source.isPlaying;
        
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.loop = false;
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
            _source.clip = audioClip;
            _source.outputAudioMixerGroup = mixerGroup;
            transform.position = position;
            _source.volume = volume;
            _source.pitch = pitch;
        }
        
        public void Clear()
        {
            _source.clip = null;
            _source.outputAudioMixerGroup = null;
            transform.position = Vector3.zero;
            _source.volume = 1f;
            _source.pitch = 1f;
        }

        public void Play() => _source.Play();
        public void Stop() => _source.Stop();
    }
}