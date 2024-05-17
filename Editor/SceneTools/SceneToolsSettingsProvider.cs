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
            
            GUILayout.Space(20f);
            EditorGUILayout.LabelField("Build Index 0 Scene", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            GUILayout.Space(5f);

            if (SceneManager.sceneCountInBuildSettings > 0)
            {
                EditorGUILayout.LabelField(SceneManager.GetSceneByBuildIndex(0).name);
            }
            else
            {
                EditorGUILayout.LabelField
                (
                    "NO SCENES ASSIGNED TO BUILD", 
                    new GUIStyle(GUI.skin.label) { normal = { textColor = Color.red } }
                );
            }
            
            var openSceneZeroOnExitingEditModeValue = OpenSceneZeroOnExitingEditMode;
            var openSceneZeroOnExitingEditModeToggle = EditorGUILayout.Toggle
            (
                "Open on exiting edit mode", 
                openSceneZeroOnExitingEditModeValue, 
                GUILayout.Width(200f)
            );
            
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