using R3;

namespace UnityAircraft.Game
{
    public class Aircraft : ActorBase
    {
        private readonly ReactiveProperty<float> _pitch = new();
        private readonly ReactiveProperty<float> _roll = new();
        private readonly ReactiveProperty<float> _yaw = new();
        private readonly ReactiveProperty<float> _throttle = new();

        public ReadOnlyReactiveProperty<float> Pitch => _pitch;
        public ReadOnlyReactiveProperty<float> Roll => _roll;
        public ReadOnlyReactiveProperty<float> Yaw => _yaw;
        public ReadOnlyReactiveProperty<float> Throttle => _throttle;

        public void SetPitch(float pitch)
        {
            _pitch.Value = pitch;
        }

        public void SetRoll(float roll)
        {
            _roll.Value = roll;
        }

        public void SetYaw(float yaw)
        {
            _yaw.Value = yaw;
        }

        public void SetThrottle(float throttle)
        {
            _throttle.Value = throttle;
        }
    }
}
