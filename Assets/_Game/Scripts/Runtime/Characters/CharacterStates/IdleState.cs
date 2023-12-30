using Managers;

public class IdleState : CharacterMainBaseState
{
    Character _character;

    public override CharacterMainStateType Type => CharacterMainStateType.Idle;

    public override void EnterState(Character character)
    {
        if (character is Player)
        {
            Player player = (Player)character;
            _character = player;

        }
        else if (character is Enemy)
        {
            Enemy enemy = (Enemy)character;
            _character = enemy;
        }
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState()
    {
    }

    public override void UpdateState()
    {
        if (_character.CheckInput() && GameManager.Instance.CurrentGameState==GameState.InGame)
        {
            if (_character is Player)
                _character.SetCharacterState(CharacterStateFactory.MoveState());
            else if (_character is AICharacter)
                _character.SetCharacterState(CharacterStateFactory.AIMoveState());
        }
    }

}
