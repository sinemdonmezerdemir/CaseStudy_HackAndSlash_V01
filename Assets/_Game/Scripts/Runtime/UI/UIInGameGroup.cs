using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Managers;
public class UIInGameGroup : UIBase
{
    public Joystick Joystick;

    public TextMeshProUGUI TxtLevel;

    private void OnEnable()
    {
        OnLevelChanged();
    }

    void OnLevelChanged()
    {
        int Level = User.Data.GetLevel();

        TxtLevel.text = Level.ToString();
    }
}
