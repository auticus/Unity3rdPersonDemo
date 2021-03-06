using System.Collections.Generic;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Combat.Targeting;
using Unity3rdPersonDemo.Locomotion;
using Unity3rdPersonDemo.Locomotion.NonPlayer;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Unity3rdPersonDemo.StateMachine
{
    public class EnemyStateMachine : StateMachine, INonPlayerMoveable, IImpactable
    {
        //todo: clean this up.  Public properties not being used outside do not need to be on the designer, esp if not tuning them.
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public List<AttackCategories> AttackTypes { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField] public ForceReceiver Force { get; private set; }
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
        [field: SerializeField] public float PlayerDetectRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }

        public GameObject Player { get; private set; }
        public NPCControlledLocomotion Locomotion { get; private set; }

        private Health _health;
        private Target _target;
        private Ragdoll _ragdoll;
        private bool _isDead = false;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            NavAgent.updatePosition = false; //do not move our models for us!
            NavAgent.updateRotation = false; //do not move our models for us!

            Locomotion = new NPCControlledLocomotion(this);
            _health = GetComponent<Health>();
            _health.OnDeath += OnDeath;
            _target = GetComponent<Target>();
            _ragdoll = GetComponent<Ragdoll>();

            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            //can visually see the range with this
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerDetectRange);
        }

        /// <inheritdoc/>
        public void PerformImpact()
        {
            if (_isDead) return;
            var defaultImpactDuration = 0.5f;
            SwitchState(new EnemyImpactState(this, defaultImpactDuration));
        }

        private void OnDeath()
        {
            _isDead = true;
            WeaponHandler.enabled = false;
            _ragdoll.ToggleRagdoll(true);
            GameObject.Destroy(_target); //simply disabling it does not clear the player from targeting it if its targeted, causing weirdness
            SwitchState(new EnemyDeadState(this));
        }
    }
}
