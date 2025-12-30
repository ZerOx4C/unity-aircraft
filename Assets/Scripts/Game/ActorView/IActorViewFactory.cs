namespace UnityAircraft.Game
{
    public interface IActorViewFactory
    {
        bool IsReady { get; }
        AircraftView CreateAircraftView();
    }
}
