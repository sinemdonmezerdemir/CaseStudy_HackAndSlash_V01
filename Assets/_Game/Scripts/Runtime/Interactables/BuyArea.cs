using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour, IInteractable
{
    public int Cost;

    protected Tween _tween, _t;

    public TextMeshProUGUI TxtCost,TxtAreatype;

    public Image ImgOutline, ImgColorBg, ImgMoney;

    Player _player;

    [SerializeField]
    AreaType AreaType;

    public void KeepInteracting(Character character)
    {
    }

    private void OnEnable()
    {
        AreaType = (AreaType) Random.Range(0, 3);

        Cost = Random.Range(50, 150);

        TxtAreatype.text= AreaType.ToString()+"+";

        TxtCost.text= Cost.ToString();
        
    }

    public void Interaction(Character character)
    {
        if (character is Player)
        {
            _player = (Player)character;

            PassiveInteraction();
        }
    }

    void PassiveInteraction()
    {
        ImgOutline.transform.DOScale(Vector3.one * 1.2f, 0.3f);

        _t = ImgColorBg.DOFillAmount(1f, 0.5f).OnUpdate(() =>
        {
            if (_player == null)
            {
                ImgColorBg.fillAmount = 0f;

                _t.Kill();
            }
        }).OnComplete(() =>
        {
            Buy(_player);
        });
        
        EnemyManager.Instance.EnemiesPause();
    }

    public void EndInteraction(Character character)
    {
        if (character is Player)
        {
            if (Cost > 3)
            {
                if (_tween != null)
                    _tween.Kill();

                _player = null;

                ImgColorBg.fillAmount = 0;

                EnemyManager.Instance.EnemiesPlay() ;
            }

            ImgOutline.transform.DOScale(Vector3.one, 0.3f);
        }

    }

    public void Buy(Player player)
    {
        int prevCost = Cost;

        EnemyManager.Instance.EnemiesPause();

        if (User.Data.GetCurrency() >= Cost)
        {
            float delay = 0;
            if (Cost > 10)
                delay = 2f;
            else if (Cost > 0 && Cost < 10)
                delay = 0.5f;

            _tween = DOTween.To(() => Cost, x => Cost = x, 0, delay).OnUpdate(
                () =>
                {
                    if (Cost != prevCost)
                    {
                        int d = Cost - prevCost;

                        User.Data.UpdateCurrency(d);

                        TxtCost.text = Cost.ToString();

                        prevCost = Cost;

                        if (Random.Range(0f, 1f) < 0.5f)
                            SoundManager.PlaySound(SoundManager.Sound.DropMoney);

                        Money money = LevelManager.Instance.GetMoneyFromPoolNonInteractable();

                        money.transform.localScale = Vector3.one * 0.3f;

                        money.transform.parent = player.MoneyParent;

                        money.transform.localPosition = Vector3.zero;

                        money.transform.parent = ImgMoney.transform;

                        money.transform.DOLocalJump(Vector3.zero, 1f, 1, 0.2f).SetEase(Ease.InBounce).OnComplete(() => { money.transform.parent = LevelManager.Instance.MoneyParent; money.gameObject.SetActive(false); });

                        money.transform.DOShakeRotation(0.2f);

                        money.transform.DOScale(Vector3.one, 0.15f);

                    }

                }).OnComplete(
                () =>
                {
                    if (Cost == 0)
                    {
                        Result();
                    }
                });

        }
        else
        {
            float delay = 0;
            if (Cost > 10)
                delay = 2;
            else if (Cost > 0 && Cost < 10)
                delay = 0.5f;
            _tween = DOTween.To(() => Cost, x => Cost = x, Cost - User.Data.GetCurrency(), delay).OnUpdate(
                () =>
                {
                    if ((int)Cost != (int)prevCost)
                    {
                        int d = Cost - prevCost;

                        User.Data.UpdateCurrency(d);

                        if (Random.Range(0f, 1f) < 0.3f)
                            SoundManager.PlaySound(SoundManager.Sound.DropMoney);

                        TxtCost.text = Cost.ToString();

                        prevCost = Cost;

                        Money money = LevelManager.Instance.GetMoneyFromPoolNonInteractable();

                        money.transform.localScale = Vector3.one * 0.3f;

                        money.transform.parent = player.MoneyParent;

                        money.transform.localPosition = Vector3.zero;

                        money.transform.parent = ImgMoney.transform;

                        money.transform.DOLocalJump(Vector3.zero, 1f, 1, 0.2f).SetEase(Ease.InBounce).OnComplete(() => { money.transform.parent = LevelManager.Instance.MoneyParent; money.gameObject.SetActive(false); });

                        money.transform.DOShakeRotation(0.2f);

                        money.transform.DOScale(Vector3.one, 0.15f);
                    }

                }).OnComplete(
            () =>
            {
                if (Cost == 0)
                {
                    Result();
                }
            });

        }

    }

    void Result() 
    {
        switch (AreaType) 
        {
            case AreaType.Damage:
                LevelManager.Instance.Player.SetFireballLevel();
                break;

            case AreaType.Speed:
                LevelManager.Instance.Player.SetMoveSpeed(0);
                break;

            case AreaType.Health:
                LevelManager.Instance.Player.SetHealth();
                break;
        }

        EnemyManager.Instance.EnemiesPlay();

        this.gameObject.SetActive(false);
    }

}

public enum AreaType
{
    Speed=0,Damage=1,Health=2
}

