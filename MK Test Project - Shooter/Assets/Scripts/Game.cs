using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] Status playerStatus;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] GameObject outcomePanel;
    [SerializeField] TextMeshProUGUI outcomeText;

    // Audio
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource victoryMusic;
    [SerializeField] AudioSource defeatMusic;

    bool gameOver;

    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update () {
        if (gameOver)
        {
            return;
        }

        if (playerStatus.Health() <= 0f)
        {
            Lose();
            gameOver = true;
        }
        else if (enemySpawner.Victory())
        {
            Win();
            gameOver = true;
        }
	}

    public void Win()
    {
        outcomePanel.SetActive(true);
        outcomeText.text = "Victory";

        if (!victoryMusic.isPlaying)
        {
            backgroundMusic.Stop();
            victoryMusic.Play();
        }
    }

    public void Lose()
    {
        outcomeText.text = "Defeat";
        outcomePanel.SetActive(true);
        playerStatus.gameObject.SetActive(false);

        if (!defeatMusic.isPlaying)
        {
            backgroundMusic.Stop();
            defeatMusic.Play();
        }
    }
}
