using System;
using System.Collections.Generic;
using DJM.Utilities;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace DJM.CoreTools.AudioSystem
{
    public class SoundManager : MonoBehaviour
    {
        private const string EmitterObjectName = "[Sound Emitter]";
        private static readonly Type[] EmitterComponents = {typeof(AudioSource), typeof(SoundEmitter)};
        
        [SerializeField] private Transform emitterParent;
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int maxLowPriorityEmitterInstances = 32;

        private readonly HashSet<SoundEmitter> _checkedOutEmitters = new();
        private readonly List<SoundEmitter> _activeHighPriorityEmitters = new();
        private readonly LinkedList<SoundEmitter> _activeLowPriorityEmitters = new();
        private IObjectPool<SoundEmitter> _pool;
        
        private void Awake() => InitializePool();
        private void Update() => ReleaseIdleEmitters();

        public SoundEmitter CheckOutEmitter()
        {
            var emitter = _pool.Get();
            _checkedOutEmitters.Add(emitter);
            return emitter;
        }
        
        public void ReturnEmitter(SoundEmitter emitter)
        {
            if (!_checkedOutEmitters.Remove(emitter)) return;
            _pool.Release(emitter);
        }
        
        public void PlayTransientSound
        (
            AudioClip audioClip,
            TransientSoundPriority priority = TransientSoundPriority.Low,
            AudioMixerGroup mixerGroup = null,
            Vector3 position = default,
            float volume = 1f,
            float pitch = 1f
        )
        {
            var emitter = priority == TransientSoundPriority.Low 
                ? GetLowPriorityEmitter() 
                : GetHighPriorityEmitter();
            
            emitter.Initialize(audioClip, mixerGroup, position, volume, pitch);
            emitter.Play();
        }
        
        private SoundEmitter GetHighPriorityEmitter()
        {
            var emitter = _pool.Get();
            _activeHighPriorityEmitters.Add(emitter);
            return emitter;
        }
        
        private SoundEmitter GetLowPriorityEmitter()
        {
            if (_activeLowPriorityEmitters.Count >= maxLowPriorityEmitterInstances)
            {
                var emitter = _activeLowPriorityEmitters.First.Value;
                emitter.Stop();
                _activeLowPriorityEmitters.RemoveFirst();
                _activeLowPriorityEmitters.AddLast(emitter);
                return emitter;
            }
            
            var newEmitter = _pool.Get();
            _activeLowPriorityEmitters.AddLast(newEmitter);
            return newEmitter;
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
            for (var i = _activeHighPriorityEmitters.Count - 1; i >= 0; i--)
            {
                var emitter = _activeHighPriorityEmitters[i];
                if(emitter.IsPlaying) continue;
                _activeHighPriorityEmitters.RemoveAt(i);
                _pool.Release(emitter);
            }

            var node = _activeLowPriorityEmitters.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (!node.Value.IsPlaying)
                {
                    _pool.Release(node.Value);
                    _activeLowPriorityEmitters.Remove(node);
                }
                node = nextNode;
            }
        }

        private SoundEmitter CreateEmitter()
        {
            var go = new GameObject(EmitterObjectName, EmitterComponents)
            {
                transform = { parent = emitterParent.OrNull() ?? transform }
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
            var emitterGameObject = emitter.OrNull()?.gameObject.OrNull();
            if(emitterGameObject is null) return;
            Destroy(emitterGameObject);
        }
    }
}