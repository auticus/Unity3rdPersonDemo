using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Unity3rdPersonDemo.StateMachine.States
{
    /// <summary>
    /// Represents the player state where they are freely moving about the board.
    /// </summary>
    internal class PlayerFreeLookState : PlayerBaseState, IGameState
    {
        private readonly int FreeLookCameraBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
        { }

        public void Enter()
        {
            StateMachine.InputReader.OnTargetingClicked += InputReaderOnTargetingClicked;
            StateMachine.Animator.Play(FreeLookCameraBlendTreeHash);
        }
        
        public void Tick(float deltaTime)
        {
            StateMachine.LocomotionComponent.ProcessInputDrivenLocomotion(deltaTime);
        }

        public void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
        }

        private void InputReaderOnTargetingClicked()
        {
            //todo: look for better way to do this other than creating new state objects over and over.  Should be able to cache these somewhere
            if (!StateMachine.ObjectTargeter.TrySelectTarget()) return;
            StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
        }
    }
}
