using UnityAircraft.Game.Attribute;
using UnityAircraft.Game.Movement;
using UnityAircraft.Test.Utility;
using UnityEngine;

namespace UnityAircraft.Test.MovementTest
{
    public class MovementTestBehaviour : MonoBehaviour
    {
        [SerializeField] private AircraftMovement _movement;

        [Header("Status")]
        [SerializeField] private float _forwardSpeed;
        [SerializeField] private float _altitudeSpeed;

        [Header("Functions")]
        [SerializeField] [Button] private bool _resetTransform;
        [SerializeField] [Button] private bool _resetRigidbody;
        [SerializeField] [Button] private bool _resetControl;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = _movement.GetComponent<Rigidbody>();
        }
#if UNITY_EDITOR
        private void Reset()
        {
            _movement = GetComponent<AircraftMovement>();
        }
#endif
        private void Update()
        {
            _forwardSpeed = Vector3.Dot(_movement.transform.forward, _rigidbody.linearVelocity);
            _altitudeSpeed = Vector3.Dot(Vector3.up, _rigidbody.linearVelocity);

            TestUtility.DoOnce(ref _resetTransform, () =>
            {
                _movement.transform.localPosition = Vector3.zero;
                _movement.transform.localRotation = Quaternion.identity;
            });

            TestUtility.DoOnce(ref _resetRigidbody, () =>
                _rigidbody.linearVelocity = Vector3.zero);

            TestUtility.DoOnce(ref _resetControl, () =>
            {
                _movement.SetPitch(0);
                _movement.SetRoll(0);
                _movement.SetYaw(0);
                _movement.SetThrottle(0);
            });
        }
    }
}
