using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CharacterMainBaseState
{
    Player _character;

    Vector2 _inputs = new Vector2();

    public override CharacterMainStateType Type => CharacterMainStateType.Move;

    public override void EnterState(Character character)
    {
        if (character is Player)
            _character = (Player)character;


        if (!_character)
            return;

        _character.SetAnimState(AnimatorBoolean.Move, true);
    }

    public override void UpdateState()
    {
        _inputs = GetInput();

        if (!_character.CheckInput())
            _character.SetCharacterState(CharacterStateFactory.IdleState());

    }
    public override void ExitState()
    {
        _character.SetAnimState(AnimatorBoolean.Move, false);
    }

    Vector2 GetInput()
    {
        float horizontalMovement = UIManager.Instance.InGameGroup.Joystick.Horizontal;
        float verticalMovement = UIManager.Instance.InGameGroup.Joystick.Vertical;

        return new Vector2(horizontalMovement, verticalMovement);
    }

    void PlayerMove()
    {
        MoveCommand moveCommand = MoveCommandFactory.CreatePlayerMoveCommand(_character.transform, new Vector3(_inputs.x, 0f, _inputs.y), _character.Speed, 10);
        moveCommand.Execute();
    }

    public override void FixedUpdateState()
    {
        PlayerMove();
    }
}
