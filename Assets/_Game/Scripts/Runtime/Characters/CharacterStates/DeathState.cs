using Managers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DeathState : CharacterMainBaseState
{
    Character _character;
    public override CharacterMainStateType Type => CharacterMainStateType.Death;

    public override async void EnterState(Character character)
    {
        _character = character;

        character.VfxDeath.Play();

        character.SetAnimState(AnimatorBoolean.Death,true);

        if (character is Player)
            GameManager.Instance.SetState(GameState.GameOver);

        if (_character is Enemy)
            await Passive();

    }

    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
        _character.SetAnimState(AnimatorBoolean.Death, false);

    }

    public override void FixedUpdateState()
    {
    }

    async Task Passive() 
    {
        Money money = LevelManager.Instance.GetMoneyFromPool();

        money.transform.position = _character.transform.position;

        money.gameObject.SetActive(true);

        await Task.Delay(2000);

        _character.SetCharacterState(CharacterStateFactory.PassiveState());
    }
}
