namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    public abstract class NonPlayerLocomotion : Locomotion
    {
        protected new INonPlayerMoveable Character => (INonPlayerMoveable)base.Character;
        protected NonPlayerLocomotion(INonPlayerMoveable character) : base(character)
        { }
    }
}
