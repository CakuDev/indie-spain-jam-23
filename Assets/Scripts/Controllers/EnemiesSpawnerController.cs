using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnerController : MonoBehaviour
{
    [SerializeField] private List<Transform> floors;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private bool isLeftSpawner;

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
            // Instantiate the enemy  and choose the floor to go
            EnemyController enemyController = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation, enemyContainer).GetComponent<EnemyController>();
            int floorToSpawn = Random.Range(0, floors.Count);
            Transform floorEntrance = floors[floorToSpawn];
            enemyController.OnSpawn(floorEntrance.position.y, floorEntrance.parent.GetComponent<FloorController>());

            // Set the correct scale
            enemyController.transform.localScale = new(isLeftSpawner ? 1 : -1, 1, 1);
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
