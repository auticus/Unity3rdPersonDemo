using Unity3rdPersonDemo.Characters;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyIdleState : EnemyBaseState, IGameState
    {
        private readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private const float ANIMATION_BLEND_TIME = 0.2f;

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
                //todo: transition to chasing state
                Debug.Log("The player is in range of my rage!!");
                return;
            }

            StateMachine.Animator.SetFloat(SpeedHash, 0, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}
