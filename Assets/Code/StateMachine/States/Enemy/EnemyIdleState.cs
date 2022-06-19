using Unity3rdPersonDemo.Locomotion;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            StateMachine.NavAgent.enabled = true; //whenever we return to an idle state, make sure nav agent is turned back on if it was turned off
            StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, ANIMATION_BLEND_TIME);
        }

        public override void Tick(float deltaTime)
        {
            //todo: apply forces here
            if (IsPlayerInRange(StateMachine.PlayerDetectRange))
            {
                StateMachine.SwitchState(new EnemyPursuitState(StateMachine));
                return;
            }

            StateMachine.Locomotion.Process(LocomotionTypes.FreeLook, deltaTime);
        }
    }
}
