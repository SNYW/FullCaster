using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float baseSpawnDelay;
    public float spawnDelay;
    public float spawnDelayLevelFactor;
    public float minSpawnDelay;

    public Vector2 minMaxY;

    public Transform enemyParent;
    public GameObject[] enemyPrefabs;

    public static SpawnManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        spawnDelay = baseSpawnDelay;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (GameManager.Instance.playing)
            {
                var newEnemy = Instantiate(GetRandomEnemyForLevel(), GetRandomSpawnPosition(), Quaternion.identity, enemyParent).GetComponent<Enemy>();
                GameManager.Instance.Enemies.Add(newEnemy);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private GameObject GetRandomEnemyForLevel()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length - 1)];
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(transform.position.x, transform.position.y,  UnityEngine.Random.Range(minMaxY.x, minMaxY.y));
    }

    public void UpdateSpawnDelay(int level)
    {
        spawnDelay = Mathf.Clamp(spawnDelay - spawnDelayLevelFactor, minSpawnDelay, int.MaxValue);
    }
}
