using System.Collections.Generic;
using R3;

namespace UnityAircraft.Game
{
    public interface IActorCollection : IEnumerable<ActorBase>
    {
        Observable<ActorBase> OnAdd { get; }
        Observable<ActorBase> OnRemove { get; }
    }
}
