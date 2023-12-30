using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : AICharacter
{
    public void OnGameStateChanged(GameState newState)
    {
        switch (newState) 
        {
            case GameState.GameOver:
                SetCharacterState(CharacterStateFactory.PassiveState());
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.TakeDamage(Data.Damage);
        }
    }

    private void OnEnable()
    {
        CurrentHealt = Data.MaxHealth;

        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        try
        {
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;

        }
        catch { }
    }
}
