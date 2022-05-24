namespace Unity3rdPersonDemo.StateMachine.States
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine StateMachine { get; }

        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
