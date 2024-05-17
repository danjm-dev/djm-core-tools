using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.Editor.SceneTools
{
    [InitializeOnLoad]
    internal static class BootstrapSceneLoader
    {
        private const string NoScenesAssignedToBuildMessage = 
            nameof(BootstrapSceneLoader) + ": No scenes assigned to build settings. Either assign scenes to build settings or disable " + 
            nameof(SceneToolsSettingsProvider.OpenSceneZeroOnExitingEditMode) + " in " + SceneToolsSettingsProvider.Path;
        
        private static readonly List<string> UnloadedScenePaths = new();
        private static int _activeSceneIndex;
        
        static BootstrapSceneLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnEnteredEditMode();
                    break;
                
                case PlayModeStateChange.ExitingEditMode:
                    OnExitingEditMode();
                    break;
                
                case PlayModeStateChange.EnteredPlayMode:
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private static void OnEnteredEditMode()
        {
            for (var i = 0; i < UnloadedScenePaths.Count; i++)
            {
                EditorSceneManager.OpenScene
                (
                    UnloadedScenePaths[i], 
                    i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive
                );
            }

            if (_activeSceneIndex < 0 || _activeSceneIndex >= SceneManager.sceneCount)
            {
                ClearUnloadedScenePaths();
                return;
            }
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(UnloadedScenePaths[_activeSceneIndex]));
            ClearUnloadedScenePaths();
        }

        
        private static void OnExitingEditMode()
        {
            ClearUnloadedScenePaths();
            
            if(!SceneToolsSettingsProvider.OpenSceneZeroOnExitingEditMode) return;

            if (SceneManager.sceneCountInBuildSettings <= 0)
            {
                Debug.LogError(NoScenesAssignedToBuildMessage);
                EditorApplication.isPlaying = false;
                return;
            }
            
            var activeScene = SceneManager.GetActiveScene();
            var anyDirtyScenes = false;
                
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                UnloadedScenePaths.Add(scene.path);

                if (scene == activeScene) _activeSceneIndex = i;
                
                if(!scene.isDirty) continue;
                anyDirtyScenes = true;
            }

            if (anyDirtyScenes && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorApplication.isPlaying = false;
                ClearUnloadedScenePaths();
                return;
            }

            EditorSceneManager.OpenScene(SceneManager.GetSceneByBuildIndex(0).path, OpenSceneMode.Single);
        }
        
        private static void ClearUnloadedScenePaths()
        {
            UnloadedScenePaths.Clear();
            _activeSceneIndex = -1;
        }
    }
}