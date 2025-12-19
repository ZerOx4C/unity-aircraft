using R3;

namespace UnityAircraft.Game.Input
{
    public interface IAircraftInputObservable
    {
        ReadOnlyReactiveProperty<float> Pitch { get; }
        ReadOnlyReactiveProperty<float> Roll { get; }
        ReadOnlyReactiveProperty<float> Yaw { get; }
        ReadOnlyReactiveProperty<float> Throttle { get; }
        ReadOnlyReactiveProperty<bool> Gun { get; }
        Observable<Unit> Launch { get; }
    }
}
