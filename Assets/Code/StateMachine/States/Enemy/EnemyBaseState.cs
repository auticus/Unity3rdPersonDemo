namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public abstract class EnemyBaseState
    {
        protected EnemyStateMachine StateMachine { get; }

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected bool IsPlayerInRange()
        {
            //dealing with squaring is more performant than forcing the engine to work with magnitude
            var playerSqrMagnitude = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
            return playerSqrMagnitude <= StateMachine.PlayerDetectRange * StateMachine.PlayerDetectRange;
        }
    }
}