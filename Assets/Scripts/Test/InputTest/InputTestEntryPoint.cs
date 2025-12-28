using R3;
using UnityAircraft.Game;
using VContainer;
using VContainer.Unity;

namespace UnityAircraft.Test
{
    public class InputTestEntryPoint : IStartable
    {
        private readonly InputTestBehaviour _inputTestBehaviour;
        private readonly IAircraftInputObservable _aircraftInputObservable;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public InputTestEntryPoint(
            InputTestBehaviour inputTestBehaviour,
            IAircraftInputObservable aircraftInputObservable)
        {
            _inputTestBehaviour = inputTestBehaviour;
            _aircraftInputObservable = aircraftInputObservable;
        }

        void IStartable.Start()
        {
            _aircraftInputObservable.Pitch
                .Subscribe(_inputTestBehaviour.SetPitch)
                .AddTo(_disposables);

            _aircraftInputObservable.Roll
                .Subscribe(_inputTestBehaviour.SetRoll)
                .AddTo(_disposables);

            _aircraftInputObservable.Yaw
                .Subscribe(_inputTestBehaviour.SetYaw)
                .AddTo(_disposables);

            _aircraftInputObservable.Gun
                .Subscribe(_inputTestBehaviour.SetGun)
                .AddTo(_disposables);

            _aircraftInputObservable.Launch
                .Subscribe(_ => _inputTestBehaviour.ShowLaunch(0.5f))
                .AddTo(_disposables);
        }
    }
}
