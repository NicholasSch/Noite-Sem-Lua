using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawnManager : MonoBehaviour
{
    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player == null)
            return;

        string currentSceneName = SceneManager.GetActiveScene().name;
        string pendingSceneName = ProgressionManager.Instance.pendingSceneName;
        string pendingSpawnPointID = ProgressionManager.Instance.pendingSpawnPointID;

        if (string.IsNullOrWhiteSpace(pendingSpawnPointID) || pendingSceneName != currentSceneName)
            return;

        SceneSpawnPoint[] spawnPoints = FindObjectsByType<SceneSpawnPoint>(FindObjectsSortMode.None);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].SpawnPointID == pendingSpawnPointID)
            {
                player.transform.position = spawnPoints[i].transform.position;
                ProgressionManager.Instance.ClearPendingSpawn();
                return;
            }
        }

        ProgressionManager.Instance.ClearPendingSpawn();
    }
}