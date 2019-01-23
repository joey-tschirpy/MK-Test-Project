using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] Status playerStatus;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] GameObject outcomePanel;
    [SerializeField] TextMeshProUGUI outcomeText; 
	
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
        outcomePanel.SetActive(true);
        outcomeText.text = "Victory";
    }

    public void Lose()
    {
        outcomeText.text = "Defeat";
        outcomePanel.SetActive(true);
        playerStatus.gameObject.SetActive(false);
    }
}
