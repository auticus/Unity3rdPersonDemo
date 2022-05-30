namespace Unity3rdPersonDemo.StateMachine.States
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine StateMachine { get; }

        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void SwitchStateToLocomotion()
        {
            if (StateMachine.ObjectTargeter.CurrentTarget != null)
            {
                StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
            }
            else
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
            }
        }
    }
}
