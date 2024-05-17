using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DJM.CoreTools.Editor.SceneTools
{
    [Overlay(typeof(SceneView), "Scene Selection")]
    internal sealed class SceneLoaderOverlay : ToolbarOverlay
    {
        private SceneLoaderOverlay() : base(SceneDropdownToggle.Id) { }
        
        [EditorToolbarElement(Id, typeof(SceneView))]
        private sealed class SceneDropdownToggle : EditorToolbarDropdown, IAccessContainerWindow
        {
            public const string Id = nameof(SceneLoaderOverlay) + "." + nameof(SceneDropdownToggle) + ".ID";
            
            private static readonly HashSet<string> LoadedSceneNameBuffer = new();
            
            public EditorWindow containerWindow { get; set; }

            private SceneDropdownToggle()
            {
                text = "Scenes";
                tooltip = "Select a scene to load";
                icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
                
                clicked += ShowSceneMenu;
            }

            private static void ShowSceneMenu()
            {
                LoadedSceneNameBuffer.Clear();
                for (var i = 0; i < SceneManager.sceneCount; i++)
                {
                    LoadedSceneNameBuffer.Add(SceneManager.GetSceneAt(i).name);
                }
                
                var activeSceneName = SceneManager.GetActiveScene().name;
                var menu = new GenericMenu();
                var sceneGuids = AssetDatabase.FindAssets("t:Scene", new []{"Assets"});
                
                for (var i = 0; i < sceneGuids.Length; i++)
                {
                    var path = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                    var name = System.IO.Path.GetFileNameWithoutExtension(path);
                    
                    // is the active scene
                    if(string.Compare(activeSceneName, name) == 0)
                    {
                        menu.AddDisabledItem(new GUIContent(name), true);
                        continue;
                    }
                    
                    var isLoaded = LoadedSceneNameBuffer.Contains(name);
                    
                    menu.AddItem(new GUIContent(name), isLoaded, () =>
                    {
                        if (isLoaded) RemoveScene(SceneManager.GetSceneByPath(path));
                        else AddScene(path);
                    });
                }
                
                menu.ShowAsContext();
            }

            private static void AddScene(string path)
            {
                EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
            }
            
            private static void RemoveScene(Scene scene)
            {
                if (!scene.isDirty)
                {
                    EditorSceneManager.CloseScene(scene, true);
                    return;
                }
                
                if (EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new []{scene}))
                {
                    EditorSceneManager.CloseScene(scene, true);
                }
            }
        }
    }
}
