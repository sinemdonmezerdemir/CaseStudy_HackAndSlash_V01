using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CharacterUpperBaseState
{
    public override CharacterUpperStateType Type => CharacterUpperStateType.Hit;

    Character _character;
    public override void EnterState(Character character)
    {
        _character = (Character)character;

        _character.SetAnimState(AnimatorBoolean.Hit, true);
    }

    public override void ExitState()
    {
        _character.SetAnimState(AnimatorBoolean.Hit, false);
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
    }
}
