using Unity3rdPersonDemo.Characters;
using Unity3rdPersonDemo.Combat;
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
            StateMachine.Locomotion.Process(LocomotionTypes.FreeLook, deltaTime);
        }

        public void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked -= InputReaderOnAttackClicked;
        }

        private void InputReaderOnTargetingClicked()
        {
            if (!StateMachine.ObjectTargeter.TrySelectTarget()) return;
            StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
        }

        private void InputReaderOnAttackClicked(AttackCategories attack)
        {
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine, GameStates.FreeLook, attack));
        }
    }
}
