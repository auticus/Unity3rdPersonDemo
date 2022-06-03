using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion
{
    /// <summary>
    /// Component that handles force and gravity on a character.
    /// </summary>
    public class ForceReceiver : MonoBehaviour
    {
        private float _gravityVelocity; //gravity
        private Vector3 _impactVelocity;
        private Vector3 _currentForceVelocity;

        [SerializeField] private CharacterController controller;
        [SerializeField] [Tooltip("Smoothing of the degradation of force, the higher it is the more slide the character has")] private float drag = 0.1f;

        /// <summary>
        /// Gets how much we should be able to move based on our gravity velocity.
        /// </summary>
        public Vector3 MovementForce => _impactVelocity + Vector3.up * _gravityVelocity;

        private void Update()
        {
            if (controller.isGrounded) HandleGroundedForce();
            else HandleInTheAirForce();

            //SmoothDamp - Gradually changes a vector toward a desired goal over time (in this case - gradually degrade the force to zero)
            _impactVelocity = Vector3.SmoothDamp(_impactVelocity, Vector3.zero, ref _currentForceVelocity, drag);
        }

        public void AddForce(Vector3 force)
        {
            _impactVelocity += force;
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
