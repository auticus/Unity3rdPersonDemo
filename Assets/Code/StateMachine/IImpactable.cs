namespace Unity3rdPersonDemo.StateMachine
{
    /// <summary>
    /// Interface applied to any object that can respond to being struck.
    /// </summary>
    public interface IImpactable
    {
        /// <summary>
        /// Processes actor to respond to an impact.
        /// </summary>
        void PerformImpact();
    }
}
