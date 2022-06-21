using System.Linq;
using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyPursuitState : EnemyBaseState
    {
        public EnemyPursuitState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, ANIMATION_BLEND_TIME);
        }

        public override void Tick(float deltaTime)
        {
            //todo: apply forces here
            if (!IsPlayerAliveAndInRange(StateMachine.PlayerDetectRange))
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
                return;
            }
            if (IsPlayerAliveAndInRange(StateMachine.AttackRange))
            {
                //todo: this will need changed to be able to intelligently cycle through what attacks are available but for now just choosing a single basic
                var attackCategory = StateMachine.AttackTypes.First();
                StateMachine.SwitchState(new EnemyAttackingState(StateMachine, attackCategory));
                return;
            }

            StateMachine.Locomotion.Process(LocomotionTypes.Pursuit, deltaTime);
        }

        public override void Exit()
        {
            //note the below could cause issues - the states can be in a race condition where force does not turn the nav agent on before the exit condition
            //so only resetting a path as needed (the demo does not check if enabled, it just always does it)
            if (StateMachine.NavAgent.enabled)
            {
                StateMachine.NavAgent.ResetPath(); //stop trying to pursue when we exit!
            }
            
            StateMachine.NavAgent.velocity = Vector3.zero;
        }
    }
}
