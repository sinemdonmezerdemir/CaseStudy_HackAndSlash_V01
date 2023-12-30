using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlwaysOnGroup : UIBase
{
    public UISettingMenu SettingMenu;
    public TextMeshProUGUI TxtCurrency;

    public Button BtnSettings, BtnUpgrade;

    private void Awake()
    {
        User.Data.OnCurrencyChanged += OnCurrencyChanged;
        BtnSettings.onClick.AddListener(FunSettins);
        BtnUpgrade.onClick.AddListener(FunUpgrade);
    }

    private void Start()
    {
        User.Data.UpdateCurrency(0);
    }

    private void OnApplicationQuit()
    {
        User.Data.OnCurrencyChanged -= OnCurrencyChanged;
    }

    void FunSettins() 
    {
        SettingMenu.Activate(true);
    }

    void FunUpgrade() 
    {
        
    }
    void OnCurrencyChanged(int amount) 
    {
        int currency = User.Data.GetCurrency();

        string formattedCurrency;

        if (currency >= 1000) // 4 basamaklý veya daha fazla
        {
            if(currency < 1000000)
                formattedCurrency = (currency / 1000f).ToString("0.00") + "K";
            else
                formattedCurrency = (currency / 1000000f).ToString("0.000") + "M";

        }
        else // 4 basamaktan az
        {
            formattedCurrency = currency.ToString();
        }

        TxtCurrency.text = formattedCurrency;
    }
}
