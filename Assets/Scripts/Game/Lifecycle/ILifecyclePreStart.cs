using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityAircraft.Game
{
    public interface ILifecyclePreStart
    {
        UniTask PreStartAsync(CancellationToken cancellation);
    }
}
