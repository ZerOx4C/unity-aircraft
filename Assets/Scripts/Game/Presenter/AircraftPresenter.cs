using R3;
using VContainer;

namespace UnityAircraft.Game
{
    public class AircraftPresenter
    {
        private readonly IAircraftInputObservable _inputObservable;

        [Inject]
        public AircraftPresenter(IAircraftInputObservable inputObservable)
        {
            _inputObservable = inputObservable;
        }

        public void Add(AircraftView view, Aircraft aircraft)
        {
            var disposables = new CompositeDisposable();
            view.destroyCancellationToken.Register(disposables.Dispose);

            _inputObservable.Pitch
                .Subscribe(aircraft.SetPitch)
                .AddTo(disposables);

            _inputObservable.Roll
                .Subscribe(aircraft.SetRoll)
                .AddTo(disposables);

            _inputObservable.Yaw
                .Subscribe(aircraft.SetYaw)
                .AddTo(disposables);

            _inputObservable.Throttle
                .Subscribe(aircraft.SetThrottle)
                .AddTo(disposables);

            aircraft.Pitch
                .Subscribe(view.Movement.SetPitch)
                .AddTo(disposables);

            aircraft.Roll
                .Subscribe(view.Movement.SetRoll)
                .AddTo(disposables);

            aircraft.Yaw
                .Subscribe(view.Movement.SetYaw)
                .AddTo(disposables);

            aircraft.Throttle
                .Subscribe(view.Movement.SetThrottle)
                .AddTo(disposables);
        }
    }
}
