using System.Collections.Generic;
using System.Linq;
using Unity3rdPersonDemo.Combat;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public class PlayerAttackingState : PlayerBaseState, IGameState
    {
        private readonly IEnumerable<AttackAnimation> _attackAnimations;

        public PlayerAttackingState(PlayerStateMachine stateMachine, AttackCategories attack) : base(stateMachine)
        {
            _attackAnimations = AttackAnimations.GetAttacksByCategory(attack);
        }

        public void Enter()
        {
            //CrossFadeInFixedTime will blend between the current animation state and the one given here.
            StateMachine.Animator.CrossFadeInFixedTime(_attackAnimations.First().AnimationName, _attackAnimations.First().AnimationCrossFadeBlend);
        }

        public void Tick(float deltaTime)
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
