using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Enemy prefabs
    [SerializeField] GameObject riflemanPrefab;

    [SerializeField] List<Transform> spawnPoints;

    // Enemy objective variables
    [SerializeField] GameObject objective;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject enemy = Instantiate(riflemanPrefab);
            enemy.transform.position = spawnPoints[i].position;
            enemy.GetComponent<Enemy>().SetObjective(objective);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
