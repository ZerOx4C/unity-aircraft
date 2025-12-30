using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace UnityAircraft.Game
{
    public class GameEntryPoint : IAsyncStartable
    {
        private readonly ILifecycleLoadAssets[] _lifecycleLoadAssetsArray;
        private readonly ILifecyclePreStart[] _lifecyclePreStartArray;
        private readonly IAssetLoader _assetLoader;
        private readonly IActorSpawner _actorSpawner;

        [Inject]
        public GameEntryPoint(
            IEnumerable<ILifecycleLoadAssets> enumerableLifecycleLoadAssets,
            IEnumerable<ILifecyclePreStart> enumerableLifecyclePreStart,
            IAssetLoader assetLoader,
            IActorSpawner actorSpawner)
        {
            _lifecycleLoadAssetsArray = enumerableLifecycleLoadAssets.ToArray();
            _lifecyclePreStartArray = enumerableLifecyclePreStart.ToArray();
            _assetLoader = assetLoader;
            _actorSpawner = actorSpawner;
        }

        async UniTask IAsyncStartable.StartAsync(CancellationToken cancellation)
        {
            await LoadAssetsAsync(cancellation);
            await _lifecyclePreStartArray.Select(e => e.PreStartAsync(cancellation));

            _actorSpawner.Spawn(new ActorData());

            // TODO: start session.
        }

        private UniTask LoadAssetsAsync(CancellationToken cancellation)
        {
            foreach (var e in _lifecycleLoadAssetsArray)
            {
                e.LoadAssets(_assetLoader);
            }

            return UniTask.WaitUntil(() => _assetLoader.IsLoaded, cancellationToken: cancellation);
        }
    }
}
