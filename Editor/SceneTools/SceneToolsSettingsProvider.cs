using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.Editor.SceneTools
{
    internal sealed class SceneToolsSettingsProvider : SettingsProvider
    {
        public const string Name = "Scene Tools";
        public const string Path = AssemblyUtils.PathBase + Name;
        public const string SettingsLocation = "Project Settings/" + Path;
        
        public static bool OpenSceneZeroOnExitingEditMode => SceneToolsSettings.OpenSceneZeroOnExitingEditMode;

        public SceneToolsSettingsProvider(string path, SettingsScope scopes) : base(path, scopes) { }

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);
            
            var openSceneZeroOnExitingEditModeValue = OpenSceneZeroOnExitingEditMode;
            var disabled = SceneManager.sceneCountInBuildSettings < 1;
            
            
            GUILayout.Space(20f);
            EditorGUI.BeginDisabledGroup(disabled);
            var openSceneZeroOnExitingEditModeToggle = EditorGUILayout.Toggle
            (
                "Preload Index 0 Scene", 
                openSceneZeroOnExitingEditModeValue, 
                GUILayout.Width(200f)
            );
            EditorGUI.EndDisabledGroup();

            
            if (disabled)
            {
                SceneToolsSettings.OpenSceneZeroOnExitingEditMode = false;
                return;
            }
            
            if (openSceneZeroOnExitingEditModeValue != openSceneZeroOnExitingEditModeToggle)
            {
                SceneToolsSettings.OpenSceneZeroOnExitingEditMode = openSceneZeroOnExitingEditModeToggle;
            }
        }
        
        [SettingsProvider]
        private static SettingsProvider CreateSceneToolsSettingsProvider()
        {
            var provider = new SceneToolsSettingsProvider(Path, SettingsScope.Project);
            return provider;
        }
    }
}