using Unity3rdPersonDemo.StateMachine;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;

namespace Assets.Code.StateMachine.States.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int _attackHash = Animator.StringToHash("Attack");

        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attackHash, ANIMATION_BLEND_TIME);
        }

        public override void Tick(float deltaTime)
        {
            
        }
    }
}
