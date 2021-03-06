using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public abstract class BaseState : IGameState
    {
        private const string ATTACK_TAG = "Attack";
        private const int DEFAULT_LAYER = 0;

        /// <summary>
        /// Will get the progress of the animation sequence that the character is currently engaged in.
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        protected float GetCurrentAnimationCompletedTime(Animator animator)
        {
            // which state are we in to which animation if blending?
            var currentState = animator.GetCurrentAnimatorStateInfo(DEFAULT_LAYER); //only using layer 0 in this project
            var nextState = animator.GetNextAnimatorStateInfo(DEFAULT_LAYER);

            if (animator.IsInTransition(DEFAULT_LAYER) && nextState.IsTag(ATTACK_TAG)) //only using layer 0
            {
                //we are transitioning to an attack so get the data from next state
                return nextState.normalizedTime;
            }
            if (!animator.IsInTransition(DEFAULT_LAYER) && currentState.IsTag(ATTACK_TAG))
            {
                //not transitioning but playing attack animation
                return currentState.normalizedTime;
            }

            return 0f;
        }

        public virtual void Enter()
        { }

        public virtual void Tick(float deltaTime)
        { }

        public virtual void Exit()
        { }
    }
}
