using UnityEngine;

namespace DJM.CoreTools.ServiceLocator
{
    public class ServiceLocatorGlobal : ServiceLocatorBootstrapper 
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        
        protected override void Bootstrap() 
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}