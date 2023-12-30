using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class User
{
    /* ------------------------------------------ */

    public static UserData Data
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserData();
                _instance.Init();
            }

            return _instance;
        }
        set => _instance = value;
    }

    public static UserData _instance;

    /* ------------------------------------------ */

    public class UserData 
    {

        /*--------------------------------------------------------------*/

        public delegate void CurrencyChangeHandler(int amount);

        public event CurrencyChangeHandler OnCurrencyChanged;

        public delegate void LevelChangeHandler();

        public event LevelChangeHandler OnLevelChanged;

        public delegate void FireballLevelChangeEvent();

        public event FireballLevelChangeEvent OnFireballLevelChanged;


        /*--------------------------------------------------------------*/

        private const string _currentLevelKey = "CurrentLevelInt", _currencyKey = "CurrencyInt";

        private static int _currentLevel = 1, _currency = 0;

        private const string _currentFireballLevelKey = "CurrentFireballLevelInt";

        private static int _currentfireballLevel = 1;



        /*--------------------------------------------------------------*/

        public int GetLevel()
        {
            if (PlayerPrefs.HasKey(_currentLevelKey))
            {
                _currentLevel = PlayerPrefs.GetInt(_currentLevelKey);
            }

            return _currentLevel;
        }

        public void SetNextLevel()
        {
            GetLevel();
            _currentLevel++;
            PlayerPrefs.SetInt(_currentLevelKey, _currentLevel);
            OnLevelChanged?.Invoke();
            PlayerPrefs.Save();
        }

        public int GetCurrency()
        {
            if (PlayerPrefs.HasKey(_currencyKey))
            {
                _currency = PlayerPrefs.GetInt(_currencyKey);
            }

            return _currency;
        }

        public void UpdateCurrency(int amount, bool b = false)
        {
            GetCurrency();
            _currency += amount;

            if (b)
                _currency = 0;

            PlayerPrefs.SetInt(_currencyKey, _currency);
            PlayerPrefs.Save();

            OnCurrencyChanged?.Invoke(amount);
        }

        public int GetFireballLevel()
        {
            if (PlayerPrefs.HasKey(_currentFireballLevelKey))
            {
                _currentfireballLevel = PlayerPrefs.GetInt(_currentFireballLevelKey);
            }

            return _currentfireballLevel;
        }

        public void UpdateFireballLevel()
        {
            GetFireballLevel();
            _currentfireballLevel++;

            PlayerPrefs.SetInt(_currentFireballLevelKey, _currentfireballLevel);
            PlayerPrefs.Save();

            OnFireballLevelChanged?.Invoke();
        }

        /*--------------------------------------------------------------*/

        public void Init()
        {
            Load().Forget();
        }

        /* ------------------------------------------ */

        private void Save()
        {
        }

        private async UniTask Load()
        {
        }

        /* ------------------------------------------ */

        public void Clear()
        {
            User.Data = null;
        }
    }
}

/* ------------------------------------------ */
