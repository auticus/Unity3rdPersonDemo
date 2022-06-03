namespace Unity3rdPersonDemo.Locomotion
{
    /// <summary>
    /// Interface applied to any Locomotion object that will process character movement.
    /// </summary>
    public interface ILocomotion
    {
        void Process(float deltaTime);
    }
}
