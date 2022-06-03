using System.Collections.Generic;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Locomotion;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public class PlayerAttackingState : PlayerBaseState, IGameState
    {
        private readonly IList<AttackAnimation> _attackAnimationChain;
        private readonly int _currentAttackChainIndex;
        private readonly AttackCategories _currentAttackCategory;
        private const string ATTACK_TAG = "Attack";
        private const int DEFAULT_LAYER = 0;
        private const int FIRST_ATTACK_INDEX = 0;
        private float _lastAnimationTime;
        private bool _forceApplied;

        /// <summary>
        /// Constructs the <see cref="PlayerAttackingState"/> object and uses the first attack in the combo chain.
        /// </summary>
        /// <param name="stateMachine">The state stateMachine holding the character data.</param>
        /// <param name="attack">The attack category that this state belongs to.</param>
        public PlayerAttackingState(PlayerStateMachine stateMachine, AttackCategories attack)
            : this(stateMachine, attack, FIRST_ATTACK_INDEX)
        { }

        /// <summary>
        /// Constructs the <see cref="PlayerAttackingState"/> object.
        /// </summary>
        /// <param name="stateMachine">The state stateMachine holding the character data.</param>
        /// <param name="attack">The attack category that this state belongs to.</param>
        /// <param name="attackIndex">The specific attack in the attack chain that this state belongs to.</param>
        public PlayerAttackingState(PlayerStateMachine stateMachine, AttackCategories attack, int attackIndex) 
            : base(stateMachine)
        {
            _attackAnimationChain = AttackAnimations.GetAttacksByCategory(attack);
            _currentAttackChainIndex = attackIndex;
            _currentAttackCategory = attack;

            StateMachine.WeaponHandler.SetAnimationDamageMultiplier(_attackAnimationChain[_currentAttackChainIndex].AttackAttributeMultiplier);
        }

        public void Enter()
        {
            StateMachine.InputReader.OnAttackClicked += TryComboAttack;

            //CrossFadeInFixedTime will blend between the current animation state and the one given here.
            StateMachine.Animator.CrossFadeInFixedTime(_attackAnimationChain[_currentAttackChainIndex].AnimationName, _attackAnimationChain[_currentAttackChainIndex].TransitionDuration);
        }

        public void Tick(float deltaTime)
        {
            StateMachine.Locomotion.Process(LocomotionTypes.Attacking, deltaTime);
            var currentAnimationTime = GetCurrentAnimationCompletedTime();
            if (currentAnimationTime >= 1f) 
            {
                //animation has concluded, move back to locomotion state that was passed in
                SwitchStateToLocomotion();
                return;
            }

            if (currentAnimationTime >= _attackAnimationChain[_currentAttackChainIndex].ForceAppliedTime)
            {
                TryApplyForce();
            }

            _lastAnimationTime = currentAnimationTime;
        }

        public void Exit()
        {
            StateMachine.InputReader.OnAttackClicked -= TryComboAttack;
        }
        
        private void TryComboAttack(AttackCategories playerAttackOption)
        {
            //attack combos depend on the attack button being pressed toward the end of the current animation
            //combos always end if you are at the end of the chain

            //if we're not in an available window to do anything right now anyway - just return out - user hit the button too fast
            if (_lastAnimationTime < _attackAnimationChain[_currentAttackChainIndex].ComboAttackWindow) return;

            //if current attack is at the end of its chain, currently right now just return out - we don't want infinite attack chains
            if (_currentAttackChainIndex == _attackAnimationChain.Count - 1) return;

            //if attackOption passed in does not match what we are doing now - load new player state with the new option and first item in that chain
            if (_currentAttackCategory != playerAttackOption)
            {
                SwitchStateToLocomotion();
                return;
            }

            //advance the character animation
            StateMachine.SwitchState(new PlayerAttackingState(StateMachine,  playerAttackOption, _currentAttackChainIndex + 1));
        }

        private void TryApplyForce()
        {
            if (_forceApplied) return;
            StateMachine.Force.AddForce(StateMachine.transform.forward * _attackAnimationChain[_currentAttackChainIndex].Force);
            _forceApplied = true;
        }

        private float GetCurrentAnimationCompletedTime()
        {
            // which state are we in to which animation if blending?
            var currentState = StateMachine.Animator.GetCurrentAnimatorStateInfo(DEFAULT_LAYER); //only using layer 0 in this project
            var nextState = StateMachine.Animator.GetNextAnimatorStateInfo(DEFAULT_LAYER);

            if (StateMachine.Animator.IsInTransition(0) && nextState.IsTag(ATTACK_TAG)) //only using layer 0
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
