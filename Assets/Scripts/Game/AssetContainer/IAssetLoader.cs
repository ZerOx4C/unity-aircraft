using UnityEngine;

namespace UnityAircraft.Game
{
    public interface IAssetLoader
    {
        bool IsLoaded { get; }
        void Load<T>(string path) where T : Object;
    }
}
