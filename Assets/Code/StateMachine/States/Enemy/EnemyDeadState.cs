using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    internal class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            Debug.Log("Enemy has died!!");
        }
    }
}
