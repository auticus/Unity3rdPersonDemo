using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public abstract class PlayerBaseState : BaseState
    {
        protected PlayerStateMachine StateMachine { get; }

        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        /// <summary>
        /// Switches the player back to a locomotion state depending on if a valid target is being held.
        /// </summary>
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
