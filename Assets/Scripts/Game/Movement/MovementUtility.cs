using UnityEngine;

namespace UnityAircraft.Game
{
    public static class MovementUtility
    {
        public static float CalcDrag(Vector3 forward, Vector3 velocity, float factor, AnimationCurve efficiencyCurve)
        {
            if (velocity.sqrMagnitude == 0)
            {
                return 0;
            }

            var ratio = Mathf.Abs(Vector3.Dot(velocity.normalized, forward));
            return factor * efficiencyCurve.Evaluate(ratio) * velocity.sqrMagnitude;
        }

        public static float CalcLift(Vector3 forward, Vector3 velocity, float factor, AnimationCurve efficiencyCurve)
        {
            if (velocity.sqrMagnitude == 0)
            {
                return 0;
            }

            var ratio = Mathf.Max(0, Vector3.Dot(velocity.normalized, forward));
            return factor * efficiencyCurve.Evaluate(ratio) * velocity.sqrMagnitude;
        }
    }
}
