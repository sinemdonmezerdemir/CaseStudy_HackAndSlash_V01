public static class CharacterStateFactory
{
    public static MoveState MoveState() 
    {
        return new MoveState();
    }

    public static DeathState DeathState() 
    {  
        return new DeathState(); 
    }

    public static PauseState PauseState()
    {
        return new PauseState();
    }
    public static IdleState IdleState() 
    {
        return new IdleState();
    }
    public static PassiveState PassiveState()
    {
        return new PassiveState();
    }

    public static ActiveState ActiveState()
    {
        return new ActiveState();
    }


    public static HandsFreeState HandsFreeState() 
    {
        return new HandsFreeState();
    }

    public static HitState HitState()
    {
        return new HitState();
    }

    public static AIMoveState AIMoveState() 
    {
        return new AIMoveState();
    }
}
