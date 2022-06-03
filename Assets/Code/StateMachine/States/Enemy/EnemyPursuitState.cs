using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyPursuitState : EnemyBaseState, IGameState
    {
        public EnemyPursuitState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
            Debug.Log("Am now pursuing");
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, ANIMATION_BLEND_TIME);
        }

        public void Tick(float deltaTime)
        {
            //todo: apply forces here
            if (!IsPlayerInRange())
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
                return;
            }

            StateMachine.Locomotion.Process(LocomotionTypes.Pursuit, deltaTime);
        }

        public void Exit()
        {
            Debug.Log("No longer pursuing");
            StateMachine.NavAgent.ResetPath(); //stop trying to pursue when we exit!
            StateMachine.NavAgent.velocity = Vector3.zero;
        }
    }
}
