using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SceneLoaders
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SceneLoader))]
    public class SceneLoaderCustomEditor : Editor
    {
        private int _selected;

        public override void OnInspectorGUI()
        {
            var sceneLoader = (SceneLoader) target;

            DrawDefaultInspector();

            EditorGUILayout.Space();
            if (GUILayout.Button("Load Scenes From Build Settings"))
            {
                SetSceneNamesFromBuildSettings(sceneLoader);
                SetSelection(sceneLoader, 0);
                
                EditorUtility.SetDirty(sceneLoader);
            }

            EditorGUILayout.Space();
            HorizontalLine();
            HandleTestSceneSelection(sceneLoader);
        }

        private void HandleTestSceneSelection(SceneLoader sceneLoader)
        {
            SetCurrentIndex(sceneLoader);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            _selected = EditorGUILayout.Popup("Test Scene:", _selected, GetSceneList(sceneLoader));
            if (EditorGUI.EndChangeCheck())
            {
                SetSelection(sceneLoader, _selected);
            }

            if (_selected != 0 && GUILayout.Button("UNSET"))
            {
                SetSelection(sceneLoader, 0);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void SetSelection(SceneLoader sceneLoader, int selection)
        {
            if (selection == 0)
            {
                sceneLoader.SetTestScene(null);
            }
            else
            {
                sceneLoader.SetTestScene(selection - 1);
            }

            EditorUtility.SetDirty(sceneLoader);
            _selected = selection;
        }

        private void SetCurrentIndex(SceneLoader sceneLoader)
        {
            var currentSelected = sceneLoader.CurrentTestSceneIndex();
            if (currentSelected == -1)
            {
                _selected = 0;
            }
            else
            {
                _selected = currentSelected + 1;
            }
        }

        private string[] GetSceneList(SceneLoader sceneLoader)
        {
            string[] allScenes = sceneLoader.GetAllScenes();

            var allScenes2 = allScenes.ToList();
            allScenes2.Insert(0, "UNSET");
            allScenes = allScenes2.ToArray();

            return allScenes;
        }

        private void SetSceneNamesFromBuildSettings(SceneLoader sceneLoader)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            string[] scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility
                    .GetScenePathByBuildIndex(i));
            }

            scenes = scenes.Where(s => s != GlobalConsts.MainSceneName && s != GlobalConsts.LoadSceneName).ToArray();
            sceneLoader.SetAllScenes(scenes);
        }

        private void HorizontalLine(int height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, height);
            rect.height = height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
    }
#endif
}