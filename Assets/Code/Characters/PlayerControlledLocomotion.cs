using Unity3rdPersonDemo.StateMachine.States;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerControlledLocomotion : Locomotion
    {
        private readonly PlayerFreeLookLocomotion _playerFreeLookLocomotion;
        private readonly PlayerTargetingLocomotion _playerTargetingLocomotion;

        public PlayerControlledLocomotion(IMoveableState character) : base(character)
        {
            _playerFreeLookLocomotion = new PlayerFreeLookLocomotion(character);
            _playerTargetingLocomotion = new PlayerTargetingLocomotion(character);
        }

        /// <inheritdoc/>
        public override void Process<T>(float deltaTime)
        {
            if (typeof(T) == typeof(PlayerFreeLookState)) _playerFreeLookLocomotion.Process(deltaTime);
            else if (typeof(T) == typeof(PlayerTargetingState)) _playerTargetingLocomotion.Process(deltaTime);
        }
    }
}
