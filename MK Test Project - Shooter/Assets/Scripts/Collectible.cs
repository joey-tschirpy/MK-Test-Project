using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour {
    [SerializeField] CollectibleData data;
    [SerializeField] Image TimerImage;
    [SerializeField] float rotationSpeed = 90f;
    float spawnTime;
    bool isCollectible;

    Renderer rend;
    Collider coll;

	// Use this for initialization
	void Start () {
        spawnTime = 0f;

        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
        Collected();
    }
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;
        Rotate(deltaTime);

        if (spawnTime < data.RespawnTime())
        {
            spawnTime += deltaTime;
            TimerImage.fillAmount = spawnTime / data.RespawnTime();
        }
        else if (!isCollectible)
        {
            Respawn();
        }
	}

    public void Collected()
    {
        TimerImage.enabled = true;
        rend.enabled = false;
        coll.enabled = false;

        spawnTime = 0f;
        isCollectible = false;
    }

    public void Respawn()
    {
        TimerImage.enabled = false;
        rend.enabled = true;
        coll.enabled = true;

        isCollectible = true;
    }

    private void Rotate(float deltaTime)
    {
        gameObject.transform.Rotate(Vector3.forward, rotationSpeed * deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if triggered by player
        if (other.gameObject.layer == 10)
        {
            Status status = other.gameObject.GetComponent<Status>();

            switch(data.StatType())
            {
                case CollectibleData.Type.Health:
                    status.AddHealth(data.Value());
                    break;
            }

            Collected();
        }
    }
}
