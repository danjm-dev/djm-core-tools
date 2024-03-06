using UnityEngine;

namespace DJM.CoreTools.ServiceLocator
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorScene : Bootstrapper 
    {
        protected override void Bootstrap() 
        {
            Container.ConfigureForScene();            
        }
    }
}