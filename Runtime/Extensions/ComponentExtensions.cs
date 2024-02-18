using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class ComponentExtensions
    {
        public static void ContextualDestroy(this Component component)
        {
            if(Application.isPlaying) Object.Destroy(component);
            else Object.DestroyImmediate(component);
        }
    }
}