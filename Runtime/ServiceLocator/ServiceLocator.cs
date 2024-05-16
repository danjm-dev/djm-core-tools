using System;
using System.Collections.Generic;
using DJM.CoreTools.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.ServiceLocator 
{
    public sealed class ServiceLocator : MonoBehaviour 
    {
        private const string GlobalServiceLocatorName = "[ServiceLocator (Global)]";
        
        private static ServiceLocator _globalContainer;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers;

        private readonly ServiceManager _services = new();
        
        /// <summary>
        /// Gets the global ServiceLocator instance. Creates new if none exists.
        /// </summary>        
        public static ServiceLocator GlobalContainer 
        {
            get 
            {
                if (_globalContainer != null) return _globalContainer;

                if (FindFirstObjectByType<ServiceLocatorGlobal>() is { } found) 
                {
                    found.BootstrapOnDemand();
                    return _globalContainer;
                }
                
                var container = new GameObject
                (
                    GlobalServiceLocatorName, 
                    typeof(ServiceLocator), 
                    typeof(ServiceLocatorGlobal)
                );
                container.GetComponent<ServiceLocatorGlobal>().BootstrapOnDemand();
                return _globalContainer;
            }
        }
        
        /// <summary>
        /// Returns the <see cref="ServiceLocator"/> configured for the scene of a MonoBehaviour. Falls back to the global instance.
        /// </summary>
        public static ServiceLocator ForSceneOf(MonoBehaviour mb)
        {
            return _sceneContainers.TryGetValue(mb.gameObject.scene, out var container) 
                ? container 
                : GlobalContainer;
        }

        /// <summary>
        /// Gets the closest ServiceLocator instance to the provided 
        /// MonoBehaviour in hierarchy, the ServiceLocator for its scene, or the global ServiceLocator.
        /// </summary>
        public static ServiceLocator For(MonoBehaviour mb) 
        {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? GlobalContainer;
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() 
        {
            _globalContainer = null;
            _sceneContainers = new Dictionary<Scene, ServiceLocator>();
        }
        
        
        
        /// <summary>
        /// Registers a service to the ServiceLocator using the service's type.
        /// </summary>
        /// <param name="service">The service to register.</param>  
        /// <typeparam name="T">Class type of the service to be registered.</typeparam>
        /// <returns>The ServiceLocator instance after registering the service.</returns>
        public ServiceLocator Register<T>(T service) 
        {
            _services.Register(service);
            return this;
        }
        
        /// <summary>
        /// Registers a service to the ServiceLocator using a specific type.
        /// </summary>
        /// <param name="type">The type to use for registration.</param>
        /// <param name="service">The service to register.</param>  
        /// <returns>The ServiceLocator instance after registering the service.</returns>
        public ServiceLocator Register(Type type, object service) 
        {
            _services.Register(type, service);
            return this;
        }
        
        /// <summary>
        /// Resolves a service of a specific type. If no service of the required type is found, an error is thrown.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>The ServiceLocator instance after attempting to retrieve the service.</returns>
        public ServiceLocator Resolve<T>(out T service) where T : class 
        {
            if (_services.TryGet(out service)) return this;

            if (!TryGetNextInHierarchy(out var container))
            {
                throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
            }
            
            container.Resolve(out service);
            return this;
        }

        /// <summary>
        /// Allows retrieval of a service of a specific type. An error is thrown if the required service does not exist.
        /// </summary>
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>Instance of the service of type T.</returns>
        public T Resolve<T>() where T : class
        {
            if (_services.TryGet(out T service)) return service;
            if (TryGetNextInHierarchy(out var container)) return container.Resolve<T>();

            throw new ArgumentException($"Could not resolve type '{typeof(T).FullName}'.");
        }
        
        /// <summary>
        /// Tries to resolve a service of a specific type. Returns whether or not the process is successful.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>True if the service retrieval was successful, false otherwise.</returns>
        public bool TryResolve<T>(out T service) where T : class 
        {
            if (_services.TryGet(out service)) return true;
            return TryGetNextInHierarchy(out var container) && container.TryResolve(out service);
        }
        
        internal void ConfigureAsGlobal(bool dontDestroyOnLoad) 
        {
            if (_globalContainer == this)
            {
                LogWarning("Already configured as global", nameof(ConfigureAsGlobal));
                return;
            }
            
            if (_globalContainer != null) 
            {
                LogWarning("Another ServiceLocator is already configured as global", nameof(ConfigureAsGlobal));
                return;
            } 

            _globalContainer = this;
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        internal void ConfigureForScene() 
        {
            if (_sceneContainers.TryAdd(gameObject.scene, this)) return;
            LogWarning("Another ServiceLocator is already configured for this scene", nameof(ConfigureForScene));
        }

        private bool TryGetNextInHierarchy(out ServiceLocator container) 
        {
            if (this == _globalContainer) 
            {
                container = null;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return container != null;
        }
        
        private void OnDestroy() 
        {
            if (this == _globalContainer) 
            {
                _globalContainer = null;
                return;
            } 
            
            if (_sceneContainers.ContainsValue(this)) 
            {
                _sceneContainers.Remove(gameObject.scene);
            }
        }
        
        private void LogWarning(string message, string nameOfMethod = null) 
        {
#if UNITY_EDITOR
            var header = string.IsNullOrEmpty(nameOfMethod) 
                ? nameof(ServiceLocator) 
                : $"{nameof(ServiceLocator)}.{nameOfMethod}";
            Debug.LogWarning($"{header}: {message}", this);
#endif
        }
    }
}