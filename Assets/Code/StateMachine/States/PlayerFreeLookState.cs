using Unity3rdPersonDemo.Combat.Targeting;
using UnityEngine;

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
            StateMachine.InputReader.OnAttackClicked += InputReaderOnAttackClicked;
            StateMachine.Animator.Play(FreeLookCameraBlendTreeHash);
        }

        public void Tick(float deltaTime)
        {
            StateMachine.Locomotion.Process<PlayerFreeLookState>(deltaTime);
        }

        public void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked -= InputReaderOnAttackClicked;
        }

        private void InputReaderOnTargetingClicked()
        {
            //todo: look for better way to do this other than creating new state objects over and over.  Should be able to cache these somewhere
            if (!StateMachine.ObjectTargeter.TrySelectTarget()) return;
            StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
        }

        private void InputReaderOnAttackClicked(AttackTypes attack)
        {
            //todo: the type of attack should be sent into the attack state to let it know what its starting with
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine));
        }
    }
}
