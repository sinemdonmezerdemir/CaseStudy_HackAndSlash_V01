using Cysharp.Threading.Tasks.Triggers;
using SceneLoaders;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        /*--------------------------------------------------------------*/

        public GameState CurrentGameState { get; private set; }
        public delegate void GameStateChangeHandler(GameState newgameState);
        public event GameStateChangeHandler OnGameStateChanged;

        /*--------------------------------------------------------------*/

        protected override void Awake()
        {
            base.Awake();   
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);

            SetLogger();

            SceneLoader.Instance.LoadScene();
        }

        /*--------------------------------------------------------------*/

        private void SetLogger()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }

        public void SetState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }

        public async void LoadLevel(bool isLevelComplate)
        {
            if (isLevelComplate)
            {
                User.Data.SetNextLevel();
            }

            //SceneLoader.Instance.LoadScene();

            await UIManager.Instance.Reset();

            SetState(GameState.MainMenu);
        }

        /*--------------------------------------------------------------*/

    }
}