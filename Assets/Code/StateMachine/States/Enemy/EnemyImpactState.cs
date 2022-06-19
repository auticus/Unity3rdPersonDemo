using Unity3rdPersonDemo.Locomotion;
using Unity3rdPersonDemo.StateMachine;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;

namespace Assets.Code.StateMachine.States.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private const float CROSS_FADE_DURATION = 0.1f;
        private float _duration = 0f;

        public EnemyImpactState(EnemyStateMachine stateMachine, float durationOfImpact) : base(stateMachine)
        {
            _duration = durationOfImpact;
        }

        public override void Enter()
        {
            // todo: this will likely be changeable depending on player is equipped with, but currently in test he only has a sword
            var animatorHash = Animator.StringToHash("Impact");
            StateMachine.Animator.CrossFadeInFixedTime(animatorHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            StateMachine.Locomotion.Process(LocomotionTypes.ImpactResponse, deltaTime);
            _duration -= deltaTime;
            if (_duration <= 0)
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            }
        }
    }
}
