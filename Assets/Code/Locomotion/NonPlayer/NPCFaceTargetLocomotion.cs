namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    public class NPCFaceTargetLocomotion : NonPlayerLocomotion
    {
        public NPCFaceTargetLocomotion(INonPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime);
            FaceTarget(Character.Player.transform);
        }
    }
}
