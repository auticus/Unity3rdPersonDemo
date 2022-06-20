using Unity3rdPersonDemo.Locomotion.NonPlayer;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    public class NPCImpactedLocomotion : NonPlayerLocomotion
    {
        public NPCImpactedLocomotion(INonPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in
        }
    }
}
