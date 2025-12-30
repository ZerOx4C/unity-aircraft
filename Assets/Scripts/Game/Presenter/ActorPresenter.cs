using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace UnityAircraft.Game
{
    public class ActorPresenter :
        ILifecycleLoadAssets,
        ILifecyclePreStart,
        IDisposable
    {
        private readonly IActorCollection _actorCollection;
        private readonly IActorViewFactory _actorViewFactory;
        private readonly AircraftPresenter _aircraftPresenter;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public ActorPresenter(
            IActorCollection actorCollection,
            IActorViewFactory actorViewFactory,
            AircraftPresenter aircraftPresenter)
        {
            _actorCollection = actorCollection;
            _actorViewFactory = actorViewFactory;
            _aircraftPresenter = aircraftPresenter;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        void ILifecycleLoadAssets.LoadAssets(IAssetLoader loader)
        {
            loader.Load<GameObject>("Aircraft0");
        }

        async UniTask ILifecyclePreStart.PreStartAsync(CancellationToken cancellation)
        {
            await UniTask.WaitUntil(() => _actorViewFactory.IsReady, cancellationToken: cancellation);

            _actorCollection.OnAdd
                .Subscribe(OnActorAdd)
                .AddTo(_disposables);
        }

        private void OnActorAdd(ActorBase actor)
        {
            switch (actor)
            {
                case Aircraft aircraft:
                {
                    var view = _actorViewFactory.CreateAircraftView();
                    _aircraftPresenter.Add(view, aircraft);
                    break;
                }
            }
        }
    }
}
