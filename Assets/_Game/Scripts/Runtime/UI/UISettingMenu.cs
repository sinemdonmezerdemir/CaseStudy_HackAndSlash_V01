using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISettingMenu : UIBase
{
    public Button BtnClose;

    private void Awake()
    {
        BtnClose.onClick.AddListener(() => FunClose());
    }

    private void FunClose()
    {
        Activate(false);
    }
}
