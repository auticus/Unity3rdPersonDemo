using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    /// <summary>
    /// State that occurs whenever the player is killed.
    /// </summary>
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            Debug.Log("I HAVE DIED!!");
        }
    }
}
