using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    /// <summary>
    /// Component that handles force and gravity.
    /// </summary>
    public class ForceReceiver : MonoBehaviour
    {
        private float _gravityVelocity; //gravity
        [SerializeField] private CharacterController controller;

        /// <summary>
        /// Gets how much we should be able to move based on our gravity velocity.
        /// </summary>
        public Vector3 MovementForce => Vector3.up * _gravityVelocity;

        private void Update()
        {
            if (controller.isGrounded) HandleGroundedForce();
            else HandleInTheAirForce();
        }

        private void HandleGroundedForce()
        {
            //if you set the gravity to 0 here, then everytime you hit a decline in the landscape the character will trigger a falling animation
            //which looks very bad
            if (_gravityVelocity < 0f) _gravityVelocity = Physics.gravity.y * Time.deltaTime;
        }

        private void HandleInTheAirForce()
        {
            _gravityVelocity += Physics.gravity.y * Time.deltaTime; //can change these settings in the Physics engine
        }
    }
}
