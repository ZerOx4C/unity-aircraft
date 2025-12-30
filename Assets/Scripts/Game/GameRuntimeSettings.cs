using UnityEngine;

namespace UnityAircraft.Game
{
    [CreateAssetMenu(menuName = "UnityAircraft/GameRuntimeSettings", fileName = "GameRuntimeSettings")]
    public class GameRuntimeSettings : ScriptableObject
    {
        [SerializeField] private AircraftView _aircraftViewPrefab;

        public AircraftView AircraftViewPrefab => _aircraftViewPrefab;
    }
}
