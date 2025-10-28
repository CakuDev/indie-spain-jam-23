using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour
{
    [SerializeField] private List<Transform> floors;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private List<Transform> spawners;

    private Coroutine spawnCoroutine;
    public float secondsToSpawn = 5f;

    private void Start()
    {
        StartSpawning();
    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsToSpawn);

            // Choose the spawn position
            int randomIndex = Random.Range(0, spawners.Count);
            Vector3 position = spawners[randomIndex].position;

            // Instantiate the enemy  and choose the floor to go
            EnemyController enemyController = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, enemyContainer).GetComponent<EnemyController>();
            int floorToSpawn = Random.Range(0, floors.Count);
            Transform floorEntrance = floors[floorToSpawn];
            enemyController.OnSpawn(floorEntrance.position.y, floorEntrance.parent.GetComponent<FloorController>());

            // Set the correct scale
            enemyController.transform.localScale = new(randomIndex == 0 ? 1 : -1, 1, 1);
        }
    }

    public void StartSpawning()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}
