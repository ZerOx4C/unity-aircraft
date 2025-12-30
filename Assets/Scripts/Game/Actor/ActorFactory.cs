using UnityEngine;
using VContainer;

namespace UnityAircraft.Game
{
    public class ActorFactory : IActorFactory
    {
        [Inject]
        public ActorFactory()
        {
        }

        ActorBase IActorFactory.Create(ActorData actorData)
        {
            return new Aircraft();
        }
    }
}
