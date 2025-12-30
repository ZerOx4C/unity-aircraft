using UnityEngine;

namespace UnityAircraft.Game
{
    public class AircraftView : MonoBehaviour
    {
        [SerializeField] private AircraftMovement _movement;

        public AircraftMovement Movement => _movement;

#if UNITY_EDITOR
        private void Reset()
        {
            _movement = GetComponent<AircraftMovement>();
        }
#endif
    }
}
