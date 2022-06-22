using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public class PlayerBlockingState : PlayerBaseState
    {
        //todo: this blocking state is locked to sword only - we will need to have equipment passed in so we know what animations to load
        private readonly int SwordOnlyBlockHash = Animator.StringToHash("SwordOnlyBlock");
        private const float CrossFadeDuration = 0.1f;

        public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            StateMachine.Health.BlockPercentage = 1; //todo: right now this just says if you're blocking its block 100% of all damage
            StateMachine.Animator.CrossFadeInFixedTime(SwordOnlyBlockHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            StateMachine.Locomotion.Process(LocomotionTypes.NoExternalMovement, deltaTime);
            if (!StateMachine.InputReader.IsBlockPressed)
            {
                SwitchStateToLocomotion();
            }
        }

        public override void Exit()
        {
            StateMachine.Health.BlockPercentage = 0;
        }
    }
}
