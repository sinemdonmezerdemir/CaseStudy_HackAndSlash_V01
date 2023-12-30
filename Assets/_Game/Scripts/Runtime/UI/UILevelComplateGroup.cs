
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UILevelComplateGroup : UIBase
{
    public Button BtnNextLevel;

    public CanvasGroup CanvasGroup;


    private void Awake()
    {
        BtnNextLevel.onClick.AddListener(() => FunNextLevel());
    }

    void FunNextLevel() 
    {
        GameManager.Instance.LoadLevel(true);
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
