using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace DJM.CoreTools.AudioSystem
{
    public sealed class SoundManager : MonoBehaviour
    {
        private const string EmitterObjectName = "[Sound Emitter]";
        private static readonly Type[] EmitterComponents = {typeof(AudioSource), typeof(SoundEmitter)};
        
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int maxFrequentSoundInstances = 32;

        private readonly List<SoundEmitter> _activeEmitters = new();
        private readonly LinkedList<SoundEmitter> _activeFrequentEmitters = new();
        private IObjectPool<SoundEmitter> _pool;
        
        private void Awake() => InitializePool();
        private void Update() => ReleaseIdleEmitters();
        
        public void PlaySound
        (
            AudioClip audioClip, 
            AudioMixerGroup mixerGroup = null, 
            Vector3 position = default, 
            float volume = 1f, 
            float pitch = 1f
        )
        {
            var emitter = _pool.Get();
            _activeEmitters.Add(emitter);
            emitter.Initialize(audioClip, mixerGroup, position, volume, pitch);
            emitter.Play();
        }
        
        public void PlayFrequentSound
        (
            AudioClip audioClip, 
            AudioMixerGroup mixerGroup = null, 
            Vector3 position = default, 
            float volume = 1f, 
            float pitch = 1f
        )
        {
            SoundEmitter emitter;
            
            if (_activeFrequentEmitters.Count >= maxFrequentSoundInstances)
            {
                emitter = _activeFrequentEmitters.First.Value;
                emitter.Stop();
                _activeFrequentEmitters.RemoveFirst();
            }
            else
            {
                emitter = _pool.Get();
            }
            
            _activeFrequentEmitters.AddLast(emitter);
            
            emitter.Initialize(audioClip, mixerGroup, position, volume, pitch);
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

        private void ReleaseIdleEmitters()
        {
            for (var i = _activeEmitters.Count - 1; i >= 0; i--)
            {
                var emitter = _activeEmitters[i];
                if(emitter.IsPlaying) continue;
                _activeEmitters.RemoveAt(i);
                _pool.Release(emitter);
            }

            var node = _activeFrequentEmitters.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (!node.Value.IsPlaying)
                {
                    _pool.Release(node.Value);
                    _activeFrequentEmitters.Remove(node);
                }
                node = nextNode;
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
            emitter.Clear();
            emitter.gameObject.SetActive(false);
        }
        
        private static void OnDestroyEmitter(SoundEmitter emitter)
        {
            Destroy(emitter.gameObject);
        }
    }
}