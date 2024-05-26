using UnityEngine;
using UnityEngine.Pool;

namespace DJM.CoreTools.Components
{
    public sealed class AudioSourcePool : MonoBehaviour
    {
        [SerializeField] private bool collectionCheck;
        [SerializeField, Min(0)] private int defaultCapacity = 16;
        [SerializeField, Min(0)] private int maxSize = 64;
        
        private ObjectPool<AudioSource> _pool;

        private void OnValidate()
        {
            if (defaultCapacity > maxSize)
            {
                maxSize = defaultCapacity;
            }
        }

        private void Awake()
        {
            _pool = new ObjectPool<AudioSource>
            (
                OnCreateAudioSource, 
                OnGetAudioSource, 
                OnReleaseAudioSource,
                OnDestroyAudioSource, 
                collectionCheck,
                defaultCapacity, 
                maxSize
            );
        }

        private void OnDestroy()
        {
            _pool?.Clear();
            _pool?.Dispose();
        }
        
        private AudioSource OnCreateAudioSource()
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            SetDefaultSettings(audioSource);
            return audioSource;
        }
        
        private static void OnGetAudioSource(AudioSource audioSource)
        {
        }
        
        private static void OnReleaseAudioSource(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
            SetDefaultSettings(audioSource);
        }

        private static void OnDestroyAudioSource(AudioSource audioSource)
        {
            if(audioSource == null) return;
            Destroy(audioSource);
        }

        private static void SetDefaultSettings(AudioSource audioSource)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1;
            audioSource.pitch = 1;
        }
        
        public AudioSource Get()
        {
            return _pool.Get();
        }
        
        public void Release(AudioSource audioSource)
        {
            _pool.Release(audioSource);
        }
    }
}