using Unity3rdPersonDemo.Characters;
using Unity3rdPersonDemo.Characters.NonPlayer;
using Unity3rdPersonDemo.Characters.Player;
using Unity3rdPersonDemo.StateMachine.States.Enemy;
using UnityEngine;

namespace Unity3rdPersonDemo.StateMachine
{
    public class EnemyStateMachine : StateMachine, IMoveable
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField] public ForceReceiver Force { get; private set; }
        [field: SerializeField] public float PlayerDetectRange { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }

        public GameObject Player { get; private set; }
        public NPCControlledLocomotion Locomotion { get; private set; }

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
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
