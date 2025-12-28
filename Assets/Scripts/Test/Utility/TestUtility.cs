using System;

namespace UnityAircraft.Test
{
    public static class TestUtility
    {
        public static void DoOnce(ref bool flag, Action action)
        {
            if (!flag)
            {
                return;
            }

            flag = false;
            action?.Invoke();
        }
    }
}
