using System;
using System.Collections.Generic;
using System.Linq;
using DJM.CoreTools.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.ServiceLocator 
{
    public class ServiceLocator : MonoBehaviour 
    {
        private const string GlobalServiceLocatorName = "ServiceLocator [Global]";
        private const string SceneServiceLocatorName = "ServiceLocator [Scene]";
        
        private static ServiceLocator _globalContainer;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers;
        private static List<GameObject> _tmpSceneGameObjects;

        private readonly ServiceManager _services = new ();

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad) 
        {
            if (_globalContainer == this) 
            {
                Debug.LogWarning("ServiceLocator.ConfigureAsGlobal: Already configured as global", this);
            } 
            else if (_globalContainer != null) 
            {
                Debug.LogError("ServiceLocator.ConfigureAsGlobal: Another ServiceLocator is already configured as global", this);
            } 
            else 
            {
                _globalContainer = this;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene() 
        {
            var scene = gameObject.scene;

            if (_sceneContainers.ContainsKey(scene)) 
            {
                Debug.LogError("ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene", this);
                return;
            }
            
            _sceneContainers.Add(scene, this);
        }
        
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
                
                var container = new GameObject(GlobalServiceLocatorName, typeof(ServiceLocator));
                container.AddComponent<ServiceLocatorGlobal>().BootstrapOnDemand();

                return _globalContainer;
            }
        }
        
        /// <summary>
        /// Returns the <see cref="ServiceLocator"/> configured for the scene of a MonoBehaviour. Falls back to the global instance.
        /// </summary>
        public static ServiceLocator ForSceneOf(MonoBehaviour mb) 
        {
            var scene = mb.gameObject.scene;
            
            if (_sceneContainers.TryGetValue(scene, out var container) && container != mb) return container;
            
            _tmpSceneGameObjects.Clear();
            scene.GetRootGameObjects(_tmpSceneGameObjects);

            foreach (var go in _tmpSceneGameObjects.Where(go => go.GetComponent<ServiceLocatorScene>() != null)) 
            {
                if (go.TryGetComponent(out ServiceLocatorScene bootstrapper) && bootstrapper.Container != mb) 
                {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.Container;
                }
            }

            return GlobalContainer;
        }

        /// <summary>
        /// Gets the closest ServiceLocator instance to the provided 
        /// MonoBehaviour in hierarchy, the ServiceLocator for its scene, or the global ServiceLocator.
        /// </summary>
        public static ServiceLocator For(MonoBehaviour mb) 
        {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? GlobalContainer;
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
        /// Gets a service of a specific type. If no service of the required type is found, an error is thrown.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>The ServiceLocator instance after attempting to retrieve the service.</returns>
        public ServiceLocator Get<T>(out T service) where T : class 
        {
            if (TryGetService(out service)) return this;
            
            if (TryGetNextInHierarchy(out var container)) 
            {
                container.Get(out service);
                return this;
            }
            
            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }

        /// <summary>
        /// Allows retrieval of a service of a specific type. An error is thrown if the required service does not exist.
        /// </summary>
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>Instance of the service of type T.</returns>
        public T Get<T>() where T : class
        {
            if (TryGetService(typeof(T), out T service)) return service;
            if (TryGetNextInHierarchy(out var container)) return container.Get<T>();

            throw new ArgumentException($"Could not resolve type '{typeof(T).FullName}'.");
        }
        
        /// <summary>
        /// Tries to get a service of a specific type. Returns whether or not the process is successful.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>True if the service retrieval was successful, false otherwise.</returns>
        public bool TryGet<T>(out T service) where T : class 
        {
            var type = typeof(T);
            service = null;

            if (TryGetService(type, out service))
                return true;

            return TryGetNextInHierarchy(out var container) && container.TryGet(out service);
        }

        private bool TryGetService<T>(out T service) where T : class 
        {
            return _services.TryGet(out service);
        }

        private bool TryGetService<T>(Type type, out T service) where T : class 
        {
            return _services.TryGet(out service);
        }

        private bool TryGetNextInHierarchy(out ServiceLocator container) 
        {
            if (this == _globalContainer) {
                container = null;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return container != null;
        }

        private void OnDestroy() 
        {
            if (this == _globalContainer) {
                _globalContainer = null;
            } else if (_sceneContainers.ContainsValue(this)) {
                _sceneContainers.Remove(gameObject.scene);
            }
        }
        
        // https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() 
        {
            _globalContainer = null;
            _sceneContainers = new Dictionary<Scene, ServiceLocator>();
            _tmpSceneGameObjects = new List<GameObject>();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Global")]
        private static void AddGlobal() 
        {
            var go = new GameObject(GlobalServiceLocatorName, typeof(ServiceLocatorGlobal));
        }

        [MenuItem("GameObject/ServiceLocator/Add Scene")]
        private static void AddScene() 
        {
            var go = new GameObject(SceneServiceLocatorName, typeof(ServiceLocatorScene));
        }
#endif
    }
}