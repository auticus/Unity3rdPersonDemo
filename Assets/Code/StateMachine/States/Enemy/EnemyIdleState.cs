using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyIdleState : EnemyBaseState, IGameState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        { }

        public void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, ANIMATION_BLEND_TIME);
        }

        public void Tick(float deltaTime)
        {
            //todo: apply forces here
            if (IsPlayerInRange())
            {
                StateMachine.SwitchState(new EnemyPursuitState(StateMachine));
                return;
            }

            StateMachine.Locomotion.Process(LocomotionTypes.FreeLook, deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}
