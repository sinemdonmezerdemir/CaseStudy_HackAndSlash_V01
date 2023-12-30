using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IInteractable
{
    [SerializeField]
    float Amount = 15;

    List<Vector3> _offsets = new List<Vector3>();

    [SerializeField]
    BuyArea BuyArea;

    private void Awake()
    {
        AddOffset();
    }

    private void OnEnable()
    {
        Invoke(nameof(BuyAreaInvoke), 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(BuyArea));
    }

    public void EndInteraction(Character character)
    {
        if (character is Player)
        {
            Player player = (Player)character;

            if (player.CurrentGround == this)
                player.CurrentGround = null;
            //Open(player);
        }
    }

    public void Interaction(Character character)
    {
        if (character is Player)
        {
            Player player = (Player)character;

            player.CurrentGround = this;

            Open(player);
        }
    }

    public void KeepInteracting(Character character)
    {
    }

    void Open(Player player)
    {
        LevelManager.Instance.CloseGround();

        for (int i = 0; i < 8; i++)
        {
            Ground ground = LevelManager.Instance.GetGround();
            Vector3 offset = _offsets[i];

            ground.transform.position = player.CurrentGround.transform.position + offset;
            ground.gameObject.SetActive(true);
        }
    }

    void AddOffset()
    {
        _offsets.Add(new Vector3(0, 0, Amount));
        _offsets.Add(new Vector3(0, 0, -Amount));
        _offsets.Add(new Vector3(Amount, 0, Amount));
        _offsets.Add(new Vector3(Amount, 0, -Amount));
        _offsets.Add(new Vector3(-Amount, 0, Amount));
        _offsets.Add(new Vector3(-Amount, 0, -Amount));
        _offsets.Add(new Vector3(Amount, 0, 0));
        _offsets.Add(new Vector3(-Amount, 0, 0));
    }

    void BuyAreaInvoke() 
    {
        if (Random.Range(0, 10) >=  4)
            BuyArea.gameObject.SetActive(true);
        else
            BuyArea.gameObject.SetActive(false);

        Invoke(nameof(BuyArea), 2f);
    }
}
