using UnityEngine;

namespace UnityAircraft.Game
{
    public interface IAssetProvider
    {
        T Get<T>(string path) where T : Object;
    }
}
