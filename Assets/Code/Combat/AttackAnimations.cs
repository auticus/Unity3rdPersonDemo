using System.Collections.Generic;
using System.Linq;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Data class that holds information pertaining to attack animations and which category they belong to.
    /// </summary>
    public static class AttackAnimations
    {
        /// <summary>
        /// Gets a tuple representing attack categories matched with their attack animations and what their index number is in their respective combo chains.
        /// </summary>
        public static AttackAnimation[] AttackData { get; private set; }

        static AttackAnimations()
        {
            AttackData = new AttackAnimation[]
            {
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack1", 0, crossFadeBlend: 0.1f, comboAttackWindow: 0.6f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack2", 1, crossFadeBlend: 0.1f, comboAttackWindow: 0.5f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack3", 2, crossFadeBlend: 0.1f, comboAttackWindow : 0f)
            };
        }

        public static IList<AttackAnimation> GetAttacksByCategory(AttackCategories category) 
            => AttackData.Where(attack => attack.Category == category).ToList();
    }

    public class AttackAnimation
    {
        public AttackCategories Category { get; }
        public string AnimationName { get; }

        /// <summary>
        /// Gets the index of the combo that this attack belongs to.
        /// </summary>
        public int ComboIndex { get; }

        /// <summary>
        /// Gets the transition blend duration.
        /// </summary>
        public float TransitionDuration { get; }

        /// <summary>
        /// The percentage of an animation where player can chain the next attack.
        /// </summary>
        public float ComboAttackWindow { get; }

        private AttackAnimation(AttackCategories category, string animationName, int comboIndex, float crossFadeBlend, float comboAttackWindow)
        {
            Category = category;
            AnimationName = animationName;
            ComboIndex = comboIndex;
            TransitionDuration = crossFadeBlend;
            ComboAttackWindow = comboAttackWindow;
        }

        /// <summary>
        /// Builds an animation that has no window (it is able to be transitioned to freely).
        /// </summary>
        /// <param name="category"></param>
        /// <param name="animationName"></param>
        /// <param name="comboIndex"></param>
        /// <param name="crossFadeBlend"></param>
        /// <returns></returns>
        public static AttackAnimation BuildAnimation(
            AttackCategories category,
            string animationName,
            int comboIndex,
            float crossFadeBlend)
            => new AttackAnimation(category, animationName, comboIndex, crossFadeBlend, 0f);

        /// <summary>
        /// Builds an attack animation.
        /// </summary>
        /// <param name="category">The category of the attack animation.</param>
        /// <param name="animationName">The name of the animation as it appears in the Unity Blend Tree.</param>
        /// <param name="comboIndex">The position on the attack chain.</param>
        /// <param name="crossFadeBlend">The amount of blending from one animation to the other.</param>
        /// <param name="comboAttackWindow">What percentage into the attack the next attack can be triggered.</param>
        /// <returns></returns>
        public static AttackAnimation BuildAnimation(
            AttackCategories category,
            string animationName,
            int comboIndex,
            float crossFadeBlend,
            float comboAttackWindow)
            => new AttackAnimation(category, animationName, comboIndex, crossFadeBlend, comboAttackWindow);
    }
}
