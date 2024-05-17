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
    public sealed class SceneSelectionOverlay : ToolbarOverlay
    {
        private SceneSelectionOverlay() : base(SceneDropdownToggle.Id) { }
        
        [EditorToolbarElement(Id, typeof(SceneView))]
        private sealed class SceneDropdownToggle : EditorToolbarDropdownToggle, IAccessContainerWindow
        {
            public const string Id = nameof(SceneSelectionOverlay) + "." + nameof(SceneDropdownToggle) + ".ID";
            
            private static readonly HashSet<string> LoadedSceneNameBuffer = new();
            
            public EditorWindow containerWindow { get; set; }

            private SceneDropdownToggle()
            {
                text = "Scenes";
                tooltip = "Select a scene to load";
                icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
                
                dropdownClicked += ShowSceneMenu;
            }

            private static void ShowSceneMenu()
            {
                LoadedSceneNameBuffer.Clear();
                for (var i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    LoadedSceneNameBuffer.Add(EditorSceneManager.GetSceneAt(i).name);
                }
                
                var activeSceneName = EditorSceneManager.GetActiveScene().name;
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
                        if (isLoaded) RemoveScene(EditorSceneManager.GetSceneByPath(path));
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
