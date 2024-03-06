using UnityEngine;

namespace DJM.CoreTools.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Global")]
    public class ServiceLocatorGlobal : Bootstrapper 
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap() 
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}