using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VContainer;

namespace UnityAircraft.Game
{
    public class ActorContainer :
        IActorCollection,
        IActorSpawner,
        IDisposable
    {
        private readonly IActorFactory _actorFactory;

        private readonly List<ActorBase> _actorList = new();
        private readonly Subject<ActorBase> _onAdd = new();
        private readonly Subject<ActorBase> _onRemove = new();

        [Inject]
        public ActorContainer(IActorFactory actorFactory)
        {
            _actorFactory = actorFactory;
        }

        Observable<ActorBase> IActorCollection.OnAdd => _onAdd;
        Observable<ActorBase> IActorCollection.OnRemove => _onRemove;

        IEnumerator<ActorBase> IEnumerable<ActorBase>.GetEnumerator()
        {
            return _actorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _actorList.GetEnumerator();
        }

        void IActorSpawner.Spawn(ActorData actorData)
        {
            var actor = _actorFactory.Create(actorData);
            actor.OnDestroy
                .Select(_ => actor)
                .Subscribe(OnActorDead);

            _actorList.Add(actor);
            _onAdd.OnNext(actor);
        }

        public void Dispose()
        {
            _onAdd.Dispose();
            _onRemove.Dispose();
        }

        private void OnActorDead(ActorBase actor)
        {
            _actorList.Remove(actor);
            _onRemove.OnNext(actor);
            actor.Dispose();
        }
    }
}
