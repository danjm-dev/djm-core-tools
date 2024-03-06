using DJM.CoreTools.Extensions;
using UnityEngine;

namespace DJM.CoreTools.ServiceLocator 
{
    [DisallowMultipleComponent, RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour 
    {
        private ServiceLocator _container;
        private bool _hasBeenBootstrapped;
        internal ServiceLocator Container => _container.OrNull() ?? (_container = GetComponent<ServiceLocator>());
        
        private void Awake() => BootstrapOnDemand();
        
        public void BootstrapOnDemand() 
        {
            if (_hasBeenBootstrapped) return;
            _hasBeenBootstrapped = true;
            Bootstrap();
        }
        
        protected abstract void Bootstrap();
    }

    [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobal : Bootstrapper 
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap() 
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
    
    [AddComponentMenu("ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorScene : Bootstrapper 
    {
        protected override void Bootstrap() 
        {
            Container.ConfigureForScene();            
        }
    }
}