using Unity3rdPersonDemo.Combat;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    /// <summary>
    /// Represents the player when they are locked on to a target.
    /// </summary>
    public class PlayerTargetingState : PlayerBaseState, IGameState
    {
        private readonly int TargetingCameraBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        { }

        public void Enter()
        {
            StateMachine.InputReader.OnTargetingClicked += InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked += InputReaderOnAttackClicked;
            StateMachine.Animator.Play(TargetingCameraBlendTreeHash);
            Debug.Log($"Targeting: {StateMachine.ObjectTargeter.CurrentTarget.name}");
        }
        
        public void Tick(float deltaTime)
        {
            //if we're in target mode but we lose our target, switch state out to free look state
            if (StateMachine.ObjectTargeter.CurrentTarget == null)
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
                return;
            }
            StateMachine.Locomotion.Process<PlayerTargetingState>(deltaTime);
            StateMachine.Locomotion.FaceTarget(StateMachine.ObjectTargeter.CurrentTarget);
        }

        public void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked -= InputReaderOnAttackClicked;
            StateMachine.ObjectTargeter.ClearTarget();
            Debug.Log("Cleared Target");
        }

        //if targeting is already active and the user clicks on targeting again - that clears it
        private void InputReaderOnTargetingClicked()
        {
            //todo: as in playerfreelookstate - find a way to cache these instead of creating new ones all the time
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
        }

        private void InputReaderOnAttackClicked(AttackCategories attack)
        {
            //todo: the type of attack should be sent into the attack state to let it know what its starting with
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, attack));
        }
    }
}
