
using Managers;
using UnityEngine.UI;

public class UIMainMenuGroup : UIBase
{
    public Button BtnTapToStart;

    private void Awake()
    {
        BtnTapToStart.onClick.AddListener(() => FunTapToStart());
    }

    void FunTapToStart() 
    {
        GameManager.Instance.SetState(GameState.InGame);
    }
}
