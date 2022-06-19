using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine.States
{
    public abstract class BaseState
    {
        private const string ATTACK_TAG = "Attack";
        private const int DEFAULT_LAYER = 0;

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
    }
}
