using UnityEngine;

namespace UnityAircraft.Game.Movement
{
    public class AircraftMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Parameters")]
        [SerializeField] [Min(0)] private float _dragFactor = 1;
        [SerializeField] private AnimationCurve _dragEfficiencyCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] [Min(0)] private float _liftFactor = 1;
        [SerializeField] private AnimationCurve _liftEfficiencyCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] [Range(0, 360)] private float _pitchAngularSpeed = 90;
        [SerializeField] [Range(0, 360)] private float _rollAngularSpeed = 180;
        [SerializeField] [Range(0, 360)] private float _yawAngularSpeed = 10;
        [SerializeField] [Min(0)] private float _minThrustAcceleration = 10;
        [SerializeField] [Min(0)] private float _maxThrustAcceleration = 100;

        [Header("Controls")]
        [SerializeField] [Range(-1, 1)] private float _pitch;
        [SerializeField] [Range(-1, 1)] private float _roll;
        [SerializeField] [Range(-1, 1)] private float _yaw;
        [SerializeField] [Range(0, 1)] private float _throttle;

#if UNITY_EDITOR
        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
#endif
        private void FixedUpdate()
        {
            var velocity = _rigidbody.linearVelocity;
            var forward = transform.forward;
            var right = transform.right;
            var up = transform.up;

            var drag = MovementUtility.CalcDrag(forward, velocity, _dragFactor, _dragEfficiencyCurve);
            var lift = MovementUtility.CalcLift(forward, velocity, _liftFactor, _liftEfficiencyCurve);
            var pitchAngle = _pitch * _pitchAngularSpeed;
            var rollAngle = _roll * _rollAngularSpeed;
            var yawAngle = _yaw * _yawAngularSpeed;
            var thrust = Mathf.Lerp(_minThrustAcceleration, _maxThrustAcceleration, _throttle);

            _rigidbody.AddForce(drag * -velocity.normalized, ForceMode.Acceleration);
            _rigidbody.AddForce(lift * up, ForceMode.Acceleration);
            _rigidbody.angularVelocity = Mathf.Deg2Rad * pitchAngle * -right +
                                         Mathf.Deg2Rad * rollAngle * -forward +
                                         Mathf.Deg2Rad * yawAngle * up;
            _rigidbody.AddForce(thrust * forward);
        }

        public void SetPitch(float pitch)
        {
            _pitch = pitch;
        }

        public void SetRoll(float roll)
        {
            _roll = roll;
        }

        public void SetYaw(float yaw)
        {
            _yaw = yaw;
        }

        public void SetThrottle(float throttle)
        {
            _throttle = throttle;
        }
    }
}
