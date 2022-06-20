namespace Unity3rdPersonDemo.StateMachine
{
    /// <summary>
    /// Interface on a player object that the AI can use to determine its status
    /// </summary>
    public interface IPlayerStatus
    {
        /// <summary>
        /// Flag to indicate if the player is dead.
        /// </summary>
        bool Dead { get; }
    }
}
