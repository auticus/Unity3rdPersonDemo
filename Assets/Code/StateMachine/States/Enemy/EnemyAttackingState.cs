using System.Collections.Generic;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Locomotion;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private const int FIRST_ATTACK_INDEX = 0;
        private const float END_OF_ANIMATION_SEQUENCE = 1f;

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
            StateMachine.WeaponHandler.SetAnimationDamageMultiplier(
                _attackAnimationChain[_currentAttackChainIndex].DamageAttributeMultiplier,
                _attackAnimationChain[_currentAttackChainIndex].KnockbackMultiplier);
        }

        public override void Enter()
        {
            StateMachine.Animator.CrossFadeInFixedTime(_attackHash, ANIMATION_BLEND_TIME);
        }

        public override void Tick(float deltaTime)
        {
            var currentAnimationTime = GetCurrentAnimationCompletedTime(StateMachine.Animator);
            if (currentAnimationTime >= END_OF_ANIMATION_SEQUENCE)
            {
                //animation has concluded, move back to locomotion state that was passed in
                StateMachine.SwitchState(new EnemyPursuitState(StateMachine));
                return;
            }

            StateMachine.Locomotion.Process(LocomotionTypes.FaceTarget, deltaTime);
        }
    }
}
