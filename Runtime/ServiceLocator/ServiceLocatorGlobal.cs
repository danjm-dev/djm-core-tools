using UnityEngine;

namespace DJM.CoreTools.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/Global")]
    public class ServiceLocatorGlobal : ServiceLocatorBootstrapper 
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap() 
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}