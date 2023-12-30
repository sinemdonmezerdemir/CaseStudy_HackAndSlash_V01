using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using GorgonizeGamesTemplate;

namespace UnityToolbarExtender.Examples
{
	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}

	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("Main", "Go To Main Scene"), ToolbarStyles.commandButtonStyle))
			{
				SceneHelper.OpenMainScene();
			}

			if(GUILayout.Button(new GUIContent("Play", "Play Current Scene"), ToolbarStyles.commandButtonStyle))
			{
				SceneHelper.PlayCurrentScene();
			}
		}
	}

	static class SceneHelper
	{
		static string sceneToOpen;

        [MenuItem("GorgonizeGames Template/Play Current Scene", priority = 1)]
        public static void PlayCurrentScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorConfigManager.SetTestScene(currentScene.name, currentScene.path);
            EditorSceneManager.OpenScene(GlobalConsts.MainScenePath);
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("GorgonizeGames Template/Play Current Scene", isValidateFunction: true, priority = 2)]
        private static bool PlayCurrentSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = SceneManager.GetActiveScene();
            return CanLoadScene(currentScene.name);
        }

        [MenuItem("GorgonizeGames Template/Go to Main Scene", priority = 12)]
        public static void OpenMainScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorSceneManager.OpenScene(GlobalConsts.MainScenePath);
        }

        [MenuItem("GorgonizeGames Template/Go to Main Scene", isValidateFunction: true, priority = 13)]
        private static bool OpenMainSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = SceneManager.GetActiveScene();
            return currentScene.name != GlobalConsts.MainSceneName;
        }

        private static bool CanLoadScene(string sceneName)
        {
            return sceneName != GlobalConsts.MainSceneName && sceneName != GlobalConsts.LoadSceneName;
        }

        static void OnUpdate()
		{
			if (sceneToOpen == null ||
			    EditorApplication.isPlaying || EditorApplication.isPaused ||
			    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			EditorApplication.update -= OnUpdate;

			if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				// need to get scene via search because the path to the scene
				// file contains the package version so it'll change over time
				string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
				if (guids.Length == 0)
				{
					Debug.LogWarning("Couldn't find scene file");
				}
				else
				{
					string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
					EditorSceneManager.OpenScene(scenePath);
					EditorApplication.isPlaying = true;
				}
			}
			sceneToOpen = null;
		}
	}
}