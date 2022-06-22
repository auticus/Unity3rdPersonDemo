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
    public class PlayerStateMachine : StateMachine, IPlayerMoveable, IImpactable, IPlayerStatus
    {
        //todo: the name of this class is a bit misleading as it is a type of State StateMachine but is also controlling movement etc.
        //its more a player controller.

        //todo: do we need all of these serialized fields?  Having to manually hook these up is not fun.

        //turns this into a field under the hood so the editor can add.
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public bool Dead { get; private set; }
        [field: SerializeField] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public Targeter ObjectTargeter { get; private set; }
        [field: SerializeField] public ForceReceiver Force { get; private set; }
        [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
        
        public Health Health { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerControlledLocomotion Locomotion { get; private set; }

        private Ragdoll _ragdoll { get; set; }

        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
            Locomotion = new PlayerControlledLocomotion(this);

            Health = GetComponent<Health>();
            Health.OnDeath += OnPlayerDeath;

            _ragdoll = GetComponent<Ragdoll>();
        }

        /// <inheritdoc/>
        public void PerformImpact()
        {
            if (Dead) return;

            // if blocking, determine if the block configuration (shield, items, poise etc) is enough to not go into an impact state
            if (!IsHitEnoughToRegisterImpact()) return;

            var defaultImpactDuration = 0.5f; //todo: weapon types and strength can affect impact
            SwitchState(new PlayerImpactState(this, defaultImpactDuration));
        }

        private void OnPlayerDeath()
        {
            Dead = true;
            WeaponHandler.enabled = false;
            _ragdoll.ToggleRagdoll(true);
            SwitchState(new PlayerDeathState(this));
        }

        private bool IsHitEnoughToRegisterImpact()
        {
            //todo: currently this just always says if blocking, don't register impact
            return false;
        }
    }
}
