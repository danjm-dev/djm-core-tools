using DJM.Utilities.Extensions;
using UnityEngine;

namespace DJM.CoreTools.ServiceLocator 
{
    [DisallowMultipleComponent, RequireComponent(typeof(ServiceLocator))]
    public abstract class ServiceLocatorBootstrapper : MonoBehaviour 
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
}