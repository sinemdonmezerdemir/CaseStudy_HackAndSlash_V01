using Managers;
public class AIMoveState : CharacterMainBaseState
{
    AICharacter _aiCharacter;
    public override CharacterMainStateType Type => CharacterMainStateType.Move;
    AIMoveCommand _moveCommand;
    public override void EnterState(Character character)
    {
        if (character is AICharacter)
            _aiCharacter = (AICharacter)character;

        if (!_aiCharacter)
            return;

        _aiCharacter.SetAnimState(AnimatorBoolean.Move, true);

        _moveCommand = MoveCommandFactory.CreateAIMoveCommand(_aiCharacter.transform, _aiCharacter.Target, _aiCharacter.Data.MoveSpeed, _aiCharacter.LookAtTarget);

    }
    public override void ExitState()
    {
        _aiCharacter.SetAnimState(AnimatorBoolean.Move, false);
    }
    public override void UpdateState()
    {
        if (!_aiCharacter.CheckInput())
        {
            _aiCharacter.SetCharacterState(CharacterStateFactory.IdleState());
        }
    }

    public override void FixedUpdateState()
    {
        _moveCommand.Execute();
    }
}
