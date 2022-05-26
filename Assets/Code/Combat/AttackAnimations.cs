using System;
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
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack1", 0, crossFadeBlend: 0.1f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack2", 1, crossFadeBlend: 0.1f),
                AttackAnimation.BuildAnimation(AttackCategories.Basic, "Attack3", 2, crossFadeBlend: 0.1f)
            };
        }

        public static IEnumerable<AttackAnimation> GetAttacksByCategory(AttackCategories category) 
            => AttackData.Where(attack => attack.Category == category);
    }

    public class AttackAnimation
    {
        public AttackCategories Category { get; }
        public string AnimationName { get; }
        public int ComboIndex { get; }
        public float AnimationCrossFadeBlend { get; }

        private AttackAnimation(AttackCategories category, string animationName, int comboIndex, float crossFadeBlend)
        {
            Category = category;
            AnimationName = animationName;
            ComboIndex = comboIndex;
            AnimationCrossFadeBlend = crossFadeBlend;
        }

        public static AttackAnimation BuildAnimation(
            AttackCategories category,
            string animationName,
            int comboIndex,
            float crossFadeBlend)
            => new AttackAnimation(category, animationName, comboIndex, crossFadeBlend);
    }
}
