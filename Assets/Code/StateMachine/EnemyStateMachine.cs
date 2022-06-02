using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }

        private void Start()
        {
            SwitchState(new EnemyIdleState(this));
        }
    }
}
