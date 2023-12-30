using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HandsFreeState : CharacterUpperBaseState
{
    public override CharacterUpperStateType Type => CharacterUpperStateType.HandsFree;

    Character _character;
    public override void EnterState(Character character)
    {
        _character=(Character) character;
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
