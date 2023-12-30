using GorgonizeGamesTemplate;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoaders
{
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        public static string CurrentLoadingSceneName;
        public static bool CanLoad = true;

        [SerializeField] private string[] allScenes;
        [HideInInspector] [SerializeField] private int _testSceneIndex;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            if (_testSceneIndex != -1 && _testSceneIndex >= allScenes.Length)
            {
                _testSceneIndex = -1;
            }
        }

        public void LoadScene()
        {
#if UNITY_EDITOR
            if (TryLoadTestScene())
            {
                return;
            }
#endif
            var sceneName = allScenes[allScenes.Count() - 1];

            LoadLoaderFor(sceneName);
        }

        private bool TryLoadTestScene()
        {
            var (sceneName, _) = EditorConfigManager.GetTestScene();
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                LoadLoaderFor(sceneName);

                return true;
            }

            if (_testSceneIndex > -1)
            {
                LoadLoaderFor(allScenes[_testSceneIndex]);

                return true;
            }

            return false;
        }

        private string GetRandomScene()
        {
            Debug.Log("Load Random Scene");
            return allScenes[Random.Range(0, allScenes.Length)];
        }

        private void LoadLoaderFor(string requestedScene)
        {
            if (!CanLoad)
            {
                return;
            }

            CanLoad = false;
            CurrentLoadingSceneName = requestedScene;
            SceneManager.LoadScene(GlobalConsts.LoadSceneName);
        }

        public void SetTestScene(int? index)
        {
            if (!index.HasValue)
            {
                _testSceneIndex = -1;
            }
            else
            {
                if (index.Value >= allScenes.Length)
                {
                    _testSceneIndex = -1;
                }
                else
                {
                    _testSceneIndex = index.Value;
                }
            }
        }

        public string[] GetAllScenes()
        {
            return allScenes;
        }

        public int CurrentTestSceneIndex()
        {
            return _testSceneIndex;
        }

        public void SetAllScenes(string[] scenes)
        {
            allScenes = scenes;
        }
    }
}