using System;
using R3;

namespace UnityAircraft.Game
{
    public abstract class ActorBase : IDisposable
    {
        private readonly Subject<Unit> _onDestroy = new();
        public Observable<Unit> OnDestroy => _onDestroy;

        public virtual void Dispose()
        {
            _onDestroy.Dispose();
        }

        public void Destroy()
        {
            _onDestroy.OnNext(Unit.Default);
        }
    }
}
