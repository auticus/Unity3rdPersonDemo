using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    public class Ragdoll : MonoBehaviour
    {
        private Animator _characterAnimator;
        private Collider[] _colliders;
        private Rigidbody[] _rigidBodies;
        private CharacterController _characterController;

        private void Start()
        {
            _characterAnimator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _colliders = GetComponentsInChildren<Collider>(includeInactive: true);
            _rigidBodies = GetComponentsInChildren<Rigidbody>(includeInactive: true);

            ToggleRagdoll(false);
        }

        public void ToggleRagdoll(bool isRagdoll)
        {
            foreach (var collider in _colliders)
            {
                if (collider.gameObject.CompareTag("Ragdoll"))
                {
                    collider.enabled = isRagdoll;
                }
            }

            foreach (var rigidbody in _rigidBodies)
            {
                if (rigidbody.gameObject.CompareTag("Ragdoll"))
                {
                    rigidbody.isKinematic = !isRagdoll;
                    rigidbody.useGravity = isRagdoll;
                }
            }

            _characterController.enabled = !isRagdoll;
            _characterAnimator.enabled = !isRagdoll;
        }
    }
}
