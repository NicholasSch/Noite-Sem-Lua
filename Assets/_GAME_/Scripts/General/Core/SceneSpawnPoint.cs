using UnityEngine;

public class SceneSpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointID = "Default";

    public string SpawnPointID => spawnPointID;
}