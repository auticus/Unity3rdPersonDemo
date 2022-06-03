namespace Unity3rdPersonDemo.Characters.Player
{
    public abstract class PlayerLocomotion : Locomotion
    {
        protected new IPlayerMoveable Character => (IPlayerMoveable)base.Character;
        protected PlayerLocomotion(IPlayerMoveable character) : base(character)
        { }
    }
}
