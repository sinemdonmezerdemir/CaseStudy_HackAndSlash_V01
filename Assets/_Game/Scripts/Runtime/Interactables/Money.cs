using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour,IInteractable
{
    public BoxCollider Collider;
    static int _moneyAmount=10;
    public void EndInteraction(Character character)
    {
    }

    public void Interaction(Character character)
    {
        if (character is Player)
        {
            User.Data.UpdateCurrency(_moneyAmount);
            Collider.enabled = false;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { this.gameObject.SetActive(false); });
        }
    }

    public void KeepInteracting(Character character)
    {
    }

}
