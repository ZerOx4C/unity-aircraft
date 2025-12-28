using System.Linq;
using System.Threading;

namespace UnityAircraft.Game
{
    public static class CancellationTokenExtensions
    {
        public static CancellationToken LinkWith(this CancellationToken self, params CancellationToken[] others)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(others.Append(self).ToArray()).Token;
        }
    }
}
