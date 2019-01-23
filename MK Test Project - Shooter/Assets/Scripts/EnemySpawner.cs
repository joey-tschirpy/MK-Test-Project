using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    [System.Serializable]
    public class Waves
    {
        [SerializeField] int riflemanCount;
        [SerializeField] int turretCount;

        public int RifleManCount()
        {
            return riflemanCount;
        }

        public int TurretCount()
        {
            return turretCount;
        }
    }

    [SerializeField] List<Waves> waves;

    // Enemy prefabs
    [SerializeField] GameObject riflemanPrefab;
    [SerializeField] GameObject turretPrefab;

    // Game variables
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemyObjective;
    [SerializeField] GameObject enemies;
    private int currentWave;
    private int enemyCount;
    private float nextWaveTimer;
    private const float START_WAVE_TIME = 3f;
    private bool waveStart;
    private bool end;

    // UI variables
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI waveStartText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    // Use this for initialization
    void Start () {
        currentWave = 0;
        waveStart = true;
        end = false;
        nextWaveTimer = 0f;
        waveStartText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        if (!Victory())
        {
            UpdateWave(deltaTime);
        }
    }

    public int CurrentWave()
    {
        return currentWave;
    }

    public int EnemyCount()
    {
        return enemyCount;
    }

    private void NextWave()
    {
        currentWave++;
        waveText.text = currentWave.ToString();
        waveStartText.text = "";
        SpawnEnemies();
        UpdateEnemyCount();
    }

    public int LastWave()
    {
        return waves.Count - 1;
    }

    private void SpawnEnemies()
    {
        SpawnRiflemen(waves[currentWave - 1].RifleManCount());
        SpawnTurrets(waves[currentWave - 1].TurretCount());
    }

    private void SpawnRiflemen(int count)
    {
        SpawnRiflemen(count, 0);
    }

    private void SpawnRiflemen(int count, int startIndex)
    {
        for (int i = 0; i < count; i++)
        {
            int index = startIndex + i;

            GameObject enemy = Instantiate(riflemanPrefab);
            enemy.transform.position = spawnPoints[index % spawnPoints.Count].position;
            enemy.GetComponent<Soldier>().SetObjective(enemyObjective);
            enemy.transform.SetParent(enemies.transform);
        }
    }

    private void SpawnTurrets(int count)
    {
        SpawnTurrets(count, 0);
    }

    private void SpawnTurrets(int count, int startIndex)
    {
        for (int i = 0; i < count; i++)
        {
            int index = startIndex + i;

            GameObject enemy = Instantiate(turretPrefab);
            enemy.transform.position = spawnPoints[index % spawnPoints.Count].position;
            enemy.GetComponent<Turret>().SetObjective(enemyObjective);
            enemy.transform.SetParent(enemies.transform);
        }
    }

    private void UpdateEnemyCount()
    {
        enemyCount = enemies.transform.childCount;
        enemyCountText.text = enemyCount.ToString();
    }

    private void UpdateWave(float deltaTime)
    {
        if (nextWaveTimer >= START_WAVE_TIME)
        {
            UpdateEnemyCount();

            if (enemyCount <= 0)
            {
                if (waveStart)
                {
                    // Start of a wave (Spawn new wave of enemies)
                    NextWave();
                    waveStart = false;
                }
                else
                {
                    // End of wave (Killed all enemies in the wave)
                    nextWaveTimer = 0f;
                    waveStart = true;
                }
            }
        }
        else
        {
            nextWaveTimer += deltaTime;

            int time = (int)(START_WAVE_TIME - nextWaveTimer) + 1;
            if (time >= 1)
            {
                waveStartText.text = time.ToString();
            }
            else
            {
                waveStartText.text = "GO";
            }
        }
    }

    public bool Victory()
    {
        return currentWave >= waves.Count && enemyCount <= 0;
    }
}
