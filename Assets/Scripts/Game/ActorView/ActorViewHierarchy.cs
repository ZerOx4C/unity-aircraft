using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace UnityAircraft.Game
{
    public class ActorViewHierarchy :
        IActorViewFactory,
        ILifecyclePreStart
    {
        private readonly IAssetProvider _assetProvider;
        private readonly AircraftView _aircraftViewPrefab;

        private Transform _root;
        private bool _isReady;

        [Inject]
        public ActorViewHierarchy(
            IAssetProvider assetProvider,
            GameRuntimeSettings gameRuntimeSettings)
        {
            _assetProvider = assetProvider;
            _aircraftViewPrefab = gameRuntimeSettings.AircraftViewPrefab;
        }

        bool IActorViewFactory.IsReady => _isReady;

        AircraftView IActorViewFactory.CreateAircraftView()
        {
            var view = Object.Instantiate(_aircraftViewPrefab, _root);
            var modelPrefab = _assetProvider.Get<GameObject>("Aircraft0");
            Object.Instantiate(modelPrefab, view.transform, false);
            return view;
        }

        UniTask ILifecyclePreStart.PreStartAsync(CancellationToken cancellation)
        {
            _root = new GameObject("Actors").transform;
            _isReady = true;
            return UniTask.CompletedTask;
        }
    }
}
