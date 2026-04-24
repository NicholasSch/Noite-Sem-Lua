using System.Collections;
using UnityEngine;

public class MistCloudsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mistCloudPrefab;
    [SerializeField] private float spawnInterval = 2.5f;

    private Coroutine spawnRoutine;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawnRoutine != null) return;
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnRoutine == null) return;
        StopCoroutine(spawnRoutine);
        spawnRoutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Instantiate(mistCloudPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}