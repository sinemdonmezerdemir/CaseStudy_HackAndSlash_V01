
using Managers;

namespace States
{
    public abstract class StateBase
    {
        public abstract void EnterState(Character character);

        public abstract void UpdateState();

        public abstract void FixedUpdateState();
        public abstract void ExitState();
    }
}


