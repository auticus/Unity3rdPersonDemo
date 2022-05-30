﻿using System.Collections.Generic;
using System.Linq;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Static data-class that holds information pertaining to attack animations and which category they belong to.
    /// </summary>
    public static class AttackAnimations
    {
        /// <summary>
        /// An Array of <see cref="AttackAnimation"/>
        /// </summary>
        public static AttackAnimation[] AttackData { get; private set; }

        static AttackAnimations()
        {
            AttackData = BuildBasicAttackComboChain();
        }

        public static IList<AttackAnimation> GetAttacksByCategory(AttackCategories category) 
            => AttackData.Where(attack => attack.Category == category).ToList();

        private static AttackAnimation[] BuildBasicAttackComboChain()
        {
            return new AttackAnimation[]
            {
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack1", 0, crossFadeBlend: 0.1f, comboAttackWindow: 0.6f, forceAppliedTime:0.35f, force: 15f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack2", 1, crossFadeBlend: 0.1f, comboAttackWindow: 0.5f, forceAppliedTime:0.35f, force: 15f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack3", 2, crossFadeBlend: 0.1f, comboAttackWindow : 0f, forceAppliedTime:0.35f, force: 20f)
            };
        }
    }

    /// <summary>
    /// Represents the animation of the attack and includes all data for that attack.
    /// </summary>
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

        /// <summary>
        /// The percentage of an animation where the force will be applied.
        /// </summary>
        public float ForceAppliedTime { get; }

        /// <summary>
        /// The base force value applied by the character when the <see cref="ForceAppliedTime"/> portion of the animation is reached.
        /// </summary>
        public float Force { get; }

        private AttackAnimation(
            AttackCategories category, 
            string animationName,
            int comboIndex, 
            float crossFadeBlend, 
            float comboAttackWindow,
            float forceAppliedTime,
            float force)
        {
            Category = category;
            AnimationName = animationName;
            ComboIndex = comboIndex;
            TransitionDuration = crossFadeBlend;
            ComboAttackWindow = comboAttackWindow;
            ForceAppliedTime = forceAppliedTime;
            Force = force;
        }

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
            float comboAttackWindow,
            float forceAppliedTime,
            float force)
            => new AttackAnimation(category, animationName, comboIndex, crossFadeBlend, comboAttackWindow, forceAppliedTime, force);
    }
}
