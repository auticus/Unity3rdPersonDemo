using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    /// <summary>
    /// Represents the player when they are locked on to a target.
    /// </summary>
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingCameraBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private bool _doNotClearTargetOnExit = false;
        private const float ANIMATION_BLEND_TIME = 0.2f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        { }
        
        public override void Enter()
        {
            StateMachine.InputReader.OnTargetingClicked += InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked += InputReaderOnAttackClicked;
            StateMachine.InputReader.OnBlockClicked += InputReaderOnBlockClicked;
            StateMachine.Animator.CrossFadeInFixedTime(TargetingCameraBlendTreeHash, ANIMATION_BLEND_TIME);
        }
        
        public override void Tick(float deltaTime)
        {
            //if we're in target mode but we lose our target, switch state out to free look state
            if (StateMachine.ObjectTargeter.CurrentTarget == null)
            {
                StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
                return;
            }
            StateMachine.Locomotion.Process(LocomotionTypes.Targeting, deltaTime);
        }

        public override void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked -= InputReaderOnAttackClicked;
            StateMachine.InputReader.OnBlockClicked -= InputReaderOnBlockClicked;
            if (!_doNotClearTargetOnExit)
            {
                StateMachine.ObjectTargeter.ClearTarget();
            }
        }

        //if targeting is already active and the user clicks on targeting again - that clears it
        private void InputReaderOnTargetingClicked()
        {
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
        }

        private void InputReaderOnAttackClicked(AttackCategories attack)
        {
            _doNotClearTargetOnExit = true;
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, attack));
        }

        private void InputReaderOnBlockClicked()
        {
            _doNotClearTargetOnExit = true;
            StateMachine.SwitchState(new PlayerBlockingState(StateMachine));
        }
    }
}
