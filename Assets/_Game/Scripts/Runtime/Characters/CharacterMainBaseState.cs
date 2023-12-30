
using States;
using UnityEngine;

public abstract class CharacterMainBaseState : StateBase
{
    public abstract CharacterMainStateType Type { get; }
}

public enum CharacterMainStateType
{
    None,Move,Idle,Passive,Death,Active,Pause
}