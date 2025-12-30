namespace UnityAircraft.Game
{
    public interface IActorFactory
    {
        ActorBase Create(ActorData actorData);
    }
}
