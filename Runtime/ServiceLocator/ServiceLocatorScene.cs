using UnityEngine;

namespace DJM.CoreTools.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/Scene")]
    public class ServiceLocatorScene : ServiceLocatorBootstrapper 
    {
        protected override void Bootstrap() 
        {
            Container.ConfigureForScene();            
        }
    }
}