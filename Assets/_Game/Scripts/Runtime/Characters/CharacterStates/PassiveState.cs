using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveState : CharacterMainBaseState
{
    public override CharacterMainStateType Type => CharacterMainStateType.Passive;

    Character _character;

    public override void EnterState(Character character)
    {
       _character = (Character)character;

        _character.SetCharacterUpperState(CharacterStateFactory.HandsFreeState());

        _character.gameObject.SetActive(false);
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
