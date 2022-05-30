﻿using Unity3rdPersonDemo.Characters;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Combat.Targeting;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    /// <summary>
    /// Represents the player when they are locked on to a target.
    /// </summary>
    public class PlayerTargetingState : PlayerBaseState, IGameState
    {
        private readonly int TargetingCameraBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private bool _doNotClearTargetOnExit = false;

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
            StateMachine.Locomotion.Process(LocomotionTypes.Targeting, deltaTime);
        }

        public void Exit()
        {
            StateMachine.InputReader.OnTargetingClicked -= InputReaderOnTargetingClicked;
            StateMachine.InputReader.OnAttackClicked -= InputReaderOnAttackClicked;
            if (!_doNotClearTargetOnExit)
            {
                StateMachine.ObjectTargeter.ClearTarget();
                Debug.Log("Cleared Target");
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
    }
}
