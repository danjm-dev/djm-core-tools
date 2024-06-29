using UnityEngine;

namespace DJM.CoreTools.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundEmitter : MonoBehaviour
    {
        private AudioSource _source;
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public float Pitch
        {
            get => _source.pitch;
            set => _source.pitch = value;
        }
        public bool IsPlaying => _source.isPlaying;
        
        private void Awake() => _source = GetComponent<AudioSource>();
        
        public void Initialize(SoundData data)
        {
            _source.clip = data.Clip;
            _source.outputAudioMixerGroup = data.MixerGroup;
            _source.loop = data.Loop;
            _source.playOnAwake = data.PlayOnAwake;
        }

        public void Play()
        {
            _source.Play();
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}