using System.Collections.Generic;
using Unity3rdPersonDemo.Combat;
using Unity3rdPersonDemo.Locomotion;
using Unity3rdPersonDemo.Locomotion.NonPlayer;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Unity3rdPersonDemo.StateMachine
{
    public class EnemyStateMachine : StateMachine, INonPlayerMoveable
    {
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

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            NavAgent.updatePosition = false; //do not move our models for us!
            NavAgent.updateRotation = false; //do not move our models for us!

            Locomotion = new NPCControlledLocomotion(this);
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            //can visually see the range with this
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerDetectRange);
        }
    }
}
