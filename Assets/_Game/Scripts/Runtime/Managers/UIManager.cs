using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        /*--------------------------------------------------------------*/

        public UIMainMenuGroup MainMenuGroup;
        public UIStartupAnim StartupAnimGroup;
        public UIInGameGroup InGameGroup;
        public UIPauseMenuGroup PauseMenuGroup;
        public UILevelComplateGroup LevelComplateGroup;
        public UIGameOverMenuGroup GameOverMenuGroup;
        public UIAlwaysOnGroup AlwaysOnGroup;

        public CanvasGroup ResetGroup;

        /*--------------------------------------------------------------*/

        private void OnEnable()
        {
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }

        public void OnGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    MainMenuGroup.Activate(true);
                    StartupAnimGroup.Activate(false);
                    InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(false);
                    AlwaysOnGroup.Activate(true);
                    AlwaysOnGroup.SettingMenu.Activate(false);
                    break;
                case GameState.StartupAnim:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(true);
                    InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(false);
                    break;
                case GameState.InGame:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(false);
                    InGameGroup.Activate(true);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(false);
                    break;

                case GameState.Pause:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(false);
                    InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(true);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(false);
                    break;

                case GameState.LevelComplate:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(false);
                    //InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(true);
                    GameOverMenuGroup.Activate(false);
                    break;

                case GameState.GameOver:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(false);
                    //InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(true);
                    break;

                case GameState.None:
                    MainMenuGroup.Activate(false);
                    StartupAnimGroup.Activate(false);
                    InGameGroup.Activate(false);
                    PauseMenuGroup.Activate(false);
                    LevelComplateGroup.Activate(false);
                    GameOverMenuGroup.Activate(false);
                    break;
            }
        }

        public async Task Reset()
        {
             await ResetGroup.DOFade(1, 0.3f).OnComplete(async () => { await ResetGroup.DOFade(0, 0.5f).AsyncWaitForCompletion(); }).AsyncWaitForCompletion(); ;
        }

        /*--------------------------------------------------------------*/

    }

    /*--------------------------------------------------------------*/

}
