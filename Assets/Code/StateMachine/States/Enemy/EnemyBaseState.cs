using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public abstract class EnemyBaseState : IGameState
    {
        protected readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
        protected readonly int SpeedHash = Animator.StringToHash("Speed");
        protected const float ANIMATION_BLEND_TIME = 0.2f;

        protected EnemyStateMachine StateMachine { get; }

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected bool IsPlayerInRange(float rangeToCheck)
        {
            //dealing with squaring is more performant than forcing the engine to work with magnitude
            var playerSqrMagnitude = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
            return playerSqrMagnitude <= rangeToCheck * rangeToCheck;
        }

        public virtual void Enter()
        { }

        public virtual void Tick(float deltaTime)
        { }

        public virtual void Exit()
        { }
    }
}