using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : UIBase
{
    public MusicType SourceType;

    public ButtonType CurrentType;
    public Button Btn;

    public RectTransform OnPlacer, OffPlacer;

    public Image ImgToggle;

    private void Awake()
    {
        Btn.onClick.AddListener(SwichCurrentType);
    }

    private void OnEnable()
    {
        if (SourceType == MusicType.Sfx)
        {
            if (SoundManager.GetSfxMul() == 1)
                SwichCurrentType(ButtonType.On);
            else
                SwichCurrentType(ButtonType.Off);
        }
        else
        {
            if (SoundManager.GetMusicMul() == 1)
                SwichCurrentType(ButtonType.On);
            else
                SwichCurrentType(ButtonType.Off);
        }

    }


    public void SwichCurrentType()
    {
        switch (CurrentType)
        {
            case ButtonType.On:
                CurrentType = ButtonType.Off;
                ImgToggle.rectTransform.SetParent(OffPlacer);
                ImgToggle.rectTransform.DOLocalMove(Vector3.zero, 0.1f);
                if (SourceType == MusicType.Sfx)
                    SoundManager.SetSfxMul(0);
                else
                    SoundManager.SetMusicMul(0);
                break;

            case ButtonType.Off:
                CurrentType = ButtonType.On;
                ImgToggle.rectTransform.SetParent(OnPlacer);
                ImgToggle.rectTransform.DOLocalMove(Vector3.zero, 0.1f);
                if (SourceType == MusicType.Sfx)
                    SoundManager.SetSfxMul(1);
                else
                    SoundManager.SetMusicMul(1);
                break;
        }
    }

    public void SwichCurrentType(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Off:
                CurrentType = ButtonType.Off;
                ImgToggle.rectTransform.SetParent(OffPlacer);
                ImgToggle.rectTransform.DOLocalMove(Vector3.zero, 0.1f);
                if (SourceType == MusicType.Sfx)
                    SoundManager.SetSfxMul(0);
                else
                    SoundManager.SetMusicMul(0);
                break;

            case ButtonType.On:
                CurrentType = ButtonType.On;
                ImgToggle.rectTransform.SetParent(OnPlacer);
                ImgToggle.rectTransform.DOLocalMove(Vector3.zero, 0.1f);
                if (SourceType == MusicType.Sfx)
                    SoundManager.SetSfxMul(1);
                else
                    SoundManager.SetMusicMul(1);
                break; 
        }
    }
}

public enum ButtonType
{
    On,
    Off,
}

public enum MusicType 
{
    Sfx,Musis
}


