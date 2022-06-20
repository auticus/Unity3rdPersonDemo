using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public abstract class EnemyBaseState : BaseState
    {
        protected readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
        protected readonly int SpeedHash = Animator.StringToHash("Speed");
        protected const float ANIMATION_BLEND_TIME = 0.2f;
        private IPlayerStatus _playerStatus; 

        protected EnemyStateMachine StateMachine { get; }

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            _playerStatus = StateMachine.Player.GetComponent<IPlayerStatus>();
        }

        /// <summary>
        /// Get a value indicating if the player registering to it is in its pursuit range.
        /// </summary>
        /// <param name="rangeToCheck"></param>
        /// <returns></returns>
        protected bool IsPlayerInRange(float rangeToCheck)
        {
            //dealing with squaring is more performant than forcing the engine to work with magnitude
            var playerSqrMagnitude = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
            return playerSqrMagnitude <= rangeToCheck * rangeToCheck;
        }

        /// <summary>
        /// Gets a value indicating whether or not the player registered to it is in range and is alive.
        /// </summary>
        /// <param name="rangeToCheck"></param>
        /// <returns></returns>
        protected bool IsPlayerAliveAndInRange(float rangeToCheck)
            => IsPlayerInRange(rangeToCheck) && !IsPlayerDead();

        /// <summary>
        /// Gets a value indicating if the player registered to it is dead.
        /// </summary>
        /// <returns></returns>
        protected bool IsPlayerDead() => _playerStatus.Dead;
    }
}