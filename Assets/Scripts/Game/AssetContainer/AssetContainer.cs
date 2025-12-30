using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using Object = UnityEngine.Object;

namespace UnityAircraft.Game
{
    public class AssetContainer :
        IAssetLoader,
        IAssetProvider
    {
        private readonly HashSet<string> _loadingPathSet = new();
        private readonly Dictionary<string, Object> _assetTable = new();
        private readonly CancellationDisposable _cancellationDisposable = new();

        [Inject]
        public AssetContainer()
        {
        }

        bool IAssetLoader.IsLoaded => _loadingPathSet.Count == 0;

        void IAssetLoader.Load<T>(string path)
        {
            LoadAsync<T>(path, _cancellationDisposable.Token).Forget();
        }

        T IAssetProvider.Get<T>(string path)
        {
            return (T)_assetTable[path];
        }

        private async UniTask LoadAsync<T>(string path, CancellationToken cancellation) where T : Object
        {
            if (_loadingPathSet.Contains(path))
            {
                return;
            }

            if (_assetTable.ContainsKey(path))
            {
                return;
            }

            _loadingPathSet.Add(path);

            if (typeof(T).IsSubclassOf(typeof(Component)))
            {
                var gameObject = await LoadAssetAsync<GameObject>(path, cancellation);
                var component = gameObject.GetComponent<T>();
                _assetTable.Add(path, component);
            }
            else
            {
                var asset = await LoadAssetAsync<T>(path, cancellation);
                _assetTable.Add(path, asset);
            }

            _loadingPathSet.Remove(path);
        }

        private static async UniTask<T> LoadAssetAsync<T>(string path, CancellationToken cancellation) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            handle.Completed += op =>
            {
                if (cancellation.IsCancellationRequested)
                {
                    op.Release();
                }
            };

            return await handle.Task.AsUniTask();
        }
    }
}
