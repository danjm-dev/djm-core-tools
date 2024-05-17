using UnityEditor;
using UnityEngine;

namespace DJM.CoreTools.Editor.SceneTools
{
    [FilePath(DJM.EditorResourcesPath + nameof(SceneToolsSettings), FilePathAttribute.Location.ProjectFolder)]
    internal sealed class SceneToolsSettings : ScriptableSingleton<SceneToolsSettings>
    {
        [SerializeField] private bool openSceneZeroOnExitingEditMode = false;

        private void Reset()
        {
            openSceneZeroOnExitingEditMode = false;
        }

        public static bool OpenSceneZeroOnExitingEditMode
        {
            get => instance.openSceneZeroOnExitingEditMode;
            set
            {
                instance.openSceneZeroOnExitingEditMode = value;
                instance.Save(true);
            }
        }
    }
}