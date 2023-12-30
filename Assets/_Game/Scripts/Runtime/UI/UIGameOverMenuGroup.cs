using UnityEngine.UI;
using Managers;
using UnityEngine;
using DG.Tweening;

public class UIGameOverMenuGroup : UIBase
{
    public Button BtnGameOver;

    public CanvasGroup CanvasGroup;

    private void Awake()
    {
        BtnGameOver.onClick.AddListener(() => FunGameOver()) ;
    }

    void FunGameOver()
    {
        GameManager.Instance.LoadLevel(false);
    }

    public override void Activate(bool activate)
    {
        if (activate)
        {
            CanvasGroup.alpha = 0;
            this.gameObject.SetActive(true);

            CanvasGroup.DOFade(1, 1.5f).SetEase(Ease.InCirc);
        }
        else
        {
            this.gameObject.SetActive(false);   
        }
    }
}
