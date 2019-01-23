using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] Status playerStatus;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] GameObject winPanel;
	
	// Update is called once per frame
	void Update () {
        if (playerStatus.Health() <= 0f)
        {
            Lose();
        }

        if (enemySpawner.Victory())
        {
            Win();
        }
	}

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        Debug.Log("you lose");
    }
}
