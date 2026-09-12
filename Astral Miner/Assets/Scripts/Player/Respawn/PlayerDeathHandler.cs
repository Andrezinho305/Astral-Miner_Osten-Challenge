using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
        public void HandleDeath()
        {
            PlayerRespawnManager respawnManager =
                FindFirstObjectByType<PlayerRespawnManager>();

            if (respawnManager != null)
            {
                respawnManager.PlayerDied();
            }

            Destroy(gameObject);
        }
    }
