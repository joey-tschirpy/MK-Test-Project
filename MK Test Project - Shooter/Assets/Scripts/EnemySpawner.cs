using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Enemy prefabs
    [SerializeField] GameObject riflemanPrefab;
    [SerializeField] GameObject turretPrefab;

    // Game variables
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemyObjective;

    private int enemyCount;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < spawnPoints.Count * 2; i++)
        {
            SpawnRifleman(i % spawnPoints.Count);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enemyDestroyed()
    {
        enemyCount--;


    }

    private void SpawnRifleman(int spawnIndex)
    {
        GameObject enemy = Instantiate(riflemanPrefab);
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.GetComponent<Soldier>().SetObjective(enemyObjective);
    }

    //private void SpawnTurret(int spawnIndex)
    //{
    //    GameObject turret = Instantiate(riflemanPrefab);
    //    turret.transform.position = spawnPoints[spawnIndex].position;
    //    turret.GetComponent<Turret>().SetObjective(enemyObjective);
    //}

    public void Wave1()
    {
        int riflemanCount = 12;

        for (int i = 0; i < riflemanCount; i++)
        {
            SpawnRifleman(i);
        }
    }
}
