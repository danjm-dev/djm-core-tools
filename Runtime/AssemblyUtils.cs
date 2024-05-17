
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DJM.CoreTools.Editor")]

namespace DJM.CoreTools
{
    internal static class AssemblyUtils
    {
        public const string PathBase = "DJM/";
        public const string ResourcesPath = "Assets/Resources/" + PathBase;
        public const string EditorResourcesPath = "Assets/Editor Default Resources/" + PathBase;
    }
}