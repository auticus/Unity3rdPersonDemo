using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    /// <summary>
    /// Locomotion state involving moving toward the player.
    /// </summary>
    public class NPCPursuitLocomotion : NonPlayerLocomotion
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed"); //pass a hash instead of a string as the int is faster

        public NPCPursuitLocomotion(INonPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            Character.Animator.SetFloat(SpeedHash, Locomotion.RUN_FORWARD, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            Character.NavAgent.destination = Character.Player.transform.position;
            HandleMovement(Character.NavAgent.desiredVelocity.normalized * Character.DefaultMovementSpeed, deltaTime);
            Character.NavAgent.velocity = Character.CharacterController.velocity;  //refresh the nav agent with the character
            FaceTarget(Character.Player.transform);
        }
    }
}
