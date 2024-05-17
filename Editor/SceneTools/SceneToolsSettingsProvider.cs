using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.Editor.SceneTools
{
    internal sealed class SceneToolsSettingsProvider : SettingsProvider
    {
        private const string OpenSceneIndexZeroOnExitingEditModeId = 
            nameof(SceneToolsSettingsProvider) + "." + nameof(OpenSceneZeroOnExitingEditMode);

        public const string Path = "Project Settings/" + DJM.PathRoot + "/Scene Tools";
        
        public static bool OpenSceneZeroOnExitingEditMode
        {
            get => EditorPrefs.GetBool(OpenSceneIndexZeroOnExitingEditModeId, false);
            set => EditorPrefs.SetBool(OpenSceneIndexZeroOnExitingEditModeId, value);
        }
        
        
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
                OpenSceneZeroOnExitingEditMode = openSceneZeroOnExitingEditModeToggle;
            }
        }
        
        [SettingsProvider]
        private static SettingsProvider CreateSceneToolsSettingsProvider()
        {
            var provider = new SceneToolsSettingsProvider($"{DJM.PathRoot}/Scene Tools", SettingsScope.Project);
            return provider;
        }
    }
}