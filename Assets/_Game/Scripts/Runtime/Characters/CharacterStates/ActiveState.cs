using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : CharacterMainBaseState
{
    public override CharacterMainStateType Type => CharacterMainStateType.Active;

    Character _character;

    public override void EnterState(Character character)
    {
        _character = (Character)character;

        _character.SetCharacterUpperState(CharacterStateFactory.HandsFreeState());

        _character.CurrentHealt = _character.Data.MaxHealth;

        _character.gameObject.SetActive(true);

        _character.transform.position = LevelManager.Instance.Player.GetRandomPos().position;

        _character.SetCharacterState(CharacterStateFactory.IdleState());
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
    }
}
