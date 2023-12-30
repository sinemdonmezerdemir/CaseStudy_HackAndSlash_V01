using Managers;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoaders
{
    public class LoadingSceneManager : MonoBehaviour
    {


        private void Start()
        {

            
            SceneLoader.CanLoad = true;
            
            StartCoroutine(LoadAsync(SceneLoader.CurrentLoadingSceneName));
        }
	
        IEnumerator LoadAsync(string sceneName)
        {
            yield return new WaitForSeconds(2);

            GC.Collect();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                SetProgress(progress);
                yield return null;
            }

            Resources.UnloadUnusedAssets();
            GC.Collect();
            GameManager.Instance.SetState(GameState.MainMenu);
        }

        private void SetProgress(float progress)
        {

        }
    }
}