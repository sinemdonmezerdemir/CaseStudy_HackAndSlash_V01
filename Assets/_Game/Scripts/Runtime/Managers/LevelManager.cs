using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Managers
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        /*---------------------------------------------------------------*/
        
        public Player Player;

        public int ActiveEnemyCount = 50;

        [SerializeField]
        int _stageEnemyCount = 150;

        [SerializeField] GameObject GroundPrefab,MoneyPrefab,NonInteractableMoney;

        public Transform GroundParent,MoneyParent;

        [SerializeField]
        List<Ground> _grounds = new List<Ground>(); 

        List<Money> _moneyPool=new List<Money>();

        List<Money>_nonInteractableMoneyPool=new List<Money>();


        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < 9; i++)
            {
                Ground ground = Instantiate(GroundPrefab,GroundParent).gameObject.GetComponent<Ground>();
                
                ground.gameObject.SetActive(false);

                _grounds.Add(ground);
            }

            for (int i=0;i<500;i++) 
            {
                Money money = Instantiate(MoneyPrefab,MoneyParent).gameObject.GetComponent<Money>();
                money.gameObject.SetActive(false);
                _moneyPool.Add(money);

                Money nonmoney = Instantiate(NonInteractableMoney,MoneyParent).gameObject.GetComponent<Money>();
                nonmoney.gameObject.SetActive(false);   
                _nonInteractableMoneyPool.Add(nonmoney);
            }
        }

        private void Start()
        {
            GameManager.Instance.SetState(GameState.MainMenu);
        }

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
            switch(newState) 
            {
                case GameState.MainMenu:
                    foreach (Money m in _moneyPool)
                    {
                        if (m.gameObject.activeInHierarchy)
                        {
                            m.gameObject.SetActive(false);
                        }
                    }
                    break;
            }
        }

        public Ground GetGround() 
        {
            for (int i = 0; i < _grounds.Count; i++) 
            {
                if (!_grounds[i].gameObject.activeInHierarchy)
                    return _grounds[i];
            }

            return null;
        }

        public void CloseGround()
        {
            for (int i = 0; i < _grounds.Count; i++)
            {
                if (_grounds[i].gameObject.activeInHierarchy)
                    if(_grounds[i]!= Player.CurrentGround)
                        _grounds[i].gameObject.SetActive(false);    
            }
        }

        public int GetStageClearEnemyCount() 
        {
            return _stageEnemyCount*User.Data.GetLevel();
        }

        public Money GetMoneyFromPool()
        {
            foreach (Money m in _moneyPool)
            {
                if (!m.gameObject.activeInHierarchy)
                {
                    m.gameObject.SetActive(true);
                    m.gameObject.transform.localScale = Vector3.one;
                    m.Collider.enabled = true;
                    return m;
                }
            }

            Money money = Instantiate(MoneyPrefab, MoneyParent).GetComponent<Money>();
            money.gameObject.SetActive(true);
           _moneyPool.Add(money);
            return money;
        }

        public Money GetMoneyFromPoolNonInteractable()
        {
            foreach (Money m in _nonInteractableMoneyPool)
            {
                if (!m.gameObject.activeInHierarchy)
                {
                    m.gameObject.SetActive(true);
                    m.transform.localScale = Vector3.one * 0.3f;
                    m.transform.rotation = Quaternion.identity;
                    return m;
                }
            }
            Money money = Instantiate(NonInteractableMoney, MoneyParent).GetComponent<Money>();
            money.gameObject.SetActive(true);
            money.transform.localScale = Vector3.one * 0.5f;
            money.transform.rotation = Quaternion.identity;
            _nonInteractableMoneyPool.Add(money);
            return money;
        }

        /*---------------------------------------------------------------*/

    }
}