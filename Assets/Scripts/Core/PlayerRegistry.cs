using UnityEngine;

namespace Core
{
    public class PlayerRegistry : MonoBehaviour
    {
        public static Transform Player { get; private set; }

        public static void RegisterPlayer(Transform playerTransform)
        {
            if (Player != null && Player != playerTransform)
            {
                Debug.LogWarning($"[PlayerRegistry] Attempting to register a Player. Previous object: {Player.name}");
            }

            Player = playerTransform;
        }

        public static void UnregisterPlayer(Transform playerTransform)
        {
            if (Player == playerTransform)
            {
                Player = null;
            }
        }
    }
}
