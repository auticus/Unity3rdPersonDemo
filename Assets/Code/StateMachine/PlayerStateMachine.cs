using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Combat.Targeting;
using Unity3rdPersonDemo.Input;
using Unity3rdPersonDemo.Locomotion;
using Unity3rdPersonDemo.Locomotion.Player;
using Unity3rdPersonDemo.StateMachine.States;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine
{
    /// <summary>
    /// Primary controller of the Player.
    /// </summary>
    public class PlayerStateMachine : StateMachine, IPlayerMoveable
    {
        //todo: the name of this class is a bit misleading as it is a type of State StateMachine but is also controlling movement etc.
        //its more a player controller.

        //turns this into a field under the hood so the editor can add.
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public Targeter ObjectTargeter { get; private set; }
        [field: SerializeField] public ForceReceiver Force { get; private set; }
        [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerControlledLocomotion Locomotion { get; private set; }

        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
            Locomotion = new PlayerControlledLocomotion(this);
        }
    }
}
