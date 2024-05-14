namespace DJM.CoreTools.ServiceLocator
{
    public class ServiceLocatorScene : ServiceLocatorBootstrapper 
    {
        protected override void Bootstrap() 
        {
            Container.ConfigureForScene();            
        }
    }
}