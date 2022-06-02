namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public abstract class EnemyBaseState
    {
        protected EnemyStateMachine StateMachine { get; }

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
