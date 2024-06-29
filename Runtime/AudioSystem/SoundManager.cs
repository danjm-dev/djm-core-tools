using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DJM.CoreTools.AudioSystem
{
    public sealed class SoundManager : MonoBehaviour
    {
        private const string EmitterObjectName = "[Sound Emitter]";
        private static readonly Type[] EmitterComponents = {typeof(AudioSource), typeof(SoundEmitter)};
        
        private readonly List<SoundEmitter> _activeEmitters = new();
        private readonly Queue<SoundEmitter> _frequentSoundEmitters = new();
        private IObjectPool<SoundEmitter> _pool;
        
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int maxSoundInstances = 32;

        private void Start() => InitializePool();
        private void Update() => ReleaseIdleEmitters();
        
        public void PlaySound(SoundData soundData, Vector3 position = default, float pitch = 1f)
        {
            var emitter = _pool.Get();
            _activeEmitters.Add(emitter);
            emitter.Initialize(soundData);
            emitter.Position = position;
            emitter.Pitch = pitch;

            if (soundData.FrequentSound) EnqueueFrequentSound(emitter);
            
            emitter.Play();
        }
        
        private void InitializePool()
        {
            _pool = new ObjectPool<SoundEmitter>
            (
                CreateEmitter, 
                OnGetEmitter, 
                OnReleaseEmitter, 
                OnDestroyEmitter, 
                collectionCheck, 
                defaultCapacity, 
                maxPoolSize
            );
        }
        
        private void EnqueueFrequentSound(SoundEmitter emitter)
        {
            if (_frequentSoundEmitters.Count >= maxSoundInstances && 
                _frequentSoundEmitters.TryDequeue(out var oldestEmitter))
            {
                // issue: may have been released but not removed from queue
                oldestEmitter.Stop();
            }
            
            _frequentSoundEmitters.Enqueue(emitter);
        }

        // issue: frequent sound emitters can be released but not removed from queue
        private void ReleaseIdleEmitters()
        {
            for (var i = _activeEmitters.Count - 1; i >= 0; i--)
            {
                var emitter = _activeEmitters[i];
                if(emitter.IsPlaying) continue;
                _activeEmitters.RemoveAt(i);
                _pool.Release(emitter);
            }
        }

        private SoundEmitter CreateEmitter()
        {
            var go = new GameObject(EmitterObjectName, EmitterComponents)
            {
                transform = { parent = transform }
            };
            go.SetActive(false);
            return go.GetComponent<SoundEmitter>();
        }
        
        private static void OnGetEmitter(SoundEmitter emitter)
        {
            emitter.gameObject.SetActive(true);
        }
        
        private static void OnReleaseEmitter(SoundEmitter emitter)
        {
            emitter.gameObject.SetActive(false);
        }
        
        private static void OnDestroyEmitter(SoundEmitter emitter)
        {
            Destroy(emitter.gameObject);
        }
    }
}