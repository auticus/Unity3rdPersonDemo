using System.Collections.Generic;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.StateMachine;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;

namespace Assets.Code.StateMachine.States.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private const int FIRST_ATTACK_INDEX = 0;
        private const int DEFAULT_LAYER = 0;
        private const string ATTACK_TAG = "Attack";

        private readonly int _attackHash = Animator.StringToHash("Attack");
        private readonly IList<AttackAnimation> _attackAnimationChain;
        private readonly int _currentAttackChainIndex;
        private readonly AttackCategories _currentAttackCategory;

        public EnemyAttackingState(EnemyStateMachine statemachine, AttackCategories attack) 
            : this(statemachine, attack, FIRST_ATTACK_INDEX)
        { }

        public EnemyAttackingState(EnemyStateMachine stateMachine, AttackCategories attack, int attackIndex) : base(stateMachine)
        {
            _attackAnimationChain = AttackAnimations.GetAttacksByCategory(attack);
            _currentAttackChainIndex = attackIndex;
            _currentAttackCategory = attack;
            StateMachine.WeaponHandler.SetAnimationDamageMultiplier(_attackAnimationChain[_currentAttackChainIndex].AttackAttributeMultiplier);
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attackHash, ANIMATION_BLEND_TIME);
        }

        public override void Tick(float deltaTime)
        {
            //todo 
        }

        private float GetCurrentAnimationCompletedTime()
        {
            // which state are we in to which animation if blending?
            var currentState = StateMachine.Animator.GetCurrentAnimatorStateInfo(DEFAULT_LAYER); //only using layer 0 in this project
            var nextState = StateMachine.Animator.GetNextAnimatorStateInfo(DEFAULT_LAYER);

            if (StateMachine.Animator.IsInTransition(DEFAULT_LAYER) && nextState.IsTag(ATTACK_TAG)) //only using layer 0
            {
                //we are transitioning to an attack so get the data from next state
                return nextState.normalizedTime;
            }
            if (!StateMachine.Animator.IsInTransition(DEFAULT_LAYER) && currentState.IsTag(ATTACK_TAG))
            {
                //not transitioning but playing attack animation
                return currentState.normalizedTime;
            }

            return 0f;
        }
    }
}
