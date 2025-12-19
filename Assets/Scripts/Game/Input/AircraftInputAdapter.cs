using R3;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using VContainer;
using VContainer.Unity;

namespace UnityAircraft.Game.Input
{
    public class AircraftInputAdapter : IAircraftInputObservable, ITickable
    {
        private readonly ReactiveProperty<float> _pitch = new();
        private readonly ReactiveProperty<float> _roll = new();
        private readonly ReactiveProperty<float> _yaw = new();
        private readonly ReactiveProperty<float> _throttle = new();
        private readonly ReactiveProperty<bool> _gun = new();
        private readonly Subject<Unit> _launch = new();

        [Inject]
        public AircraftInputAdapter()
        {
        }

        ReadOnlyReactiveProperty<float> IAircraftInputObservable.Pitch => _pitch;
        ReadOnlyReactiveProperty<float> IAircraftInputObservable.Roll => _roll;
        ReadOnlyReactiveProperty<float> IAircraftInputObservable.Yaw => _yaw;
        ReadOnlyReactiveProperty<float> IAircraftInputObservable.Throttle => _throttle;
        ReadOnlyReactiveProperty<bool> IAircraftInputObservable.Gun => _gun;
        Observable<Unit> IAircraftInputObservable.Launch => _launch;

        void ITickable.Tick()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            _pitch.Value = GetAxisValue(keyboard.wKey, keyboard.sKey);
            _roll.Value = GetAxisValue(keyboard.aKey, keyboard.dKey);
            _yaw.Value = GetAxisValue(keyboard.eKey, keyboard.qKey);
            _throttle.Value = GetAxisValue(keyboard.leftShiftKey, keyboard.leftCtrlKey);
            _gun.Value = keyboard.spaceKey.isPressed;

            if (keyboard.rightShiftKey.wasPressedThisFrame)
            {
                _launch.OnNext(Unit.Default);
            }
        }

        private static float GetAxisValue(KeyControl positiveKey, KeyControl negativeKey)
        {
            return (positiveKey.IsPressed() ? 1 : 0) + (negativeKey.IsPressed() ? -1 : 0);
        }
    }
}
