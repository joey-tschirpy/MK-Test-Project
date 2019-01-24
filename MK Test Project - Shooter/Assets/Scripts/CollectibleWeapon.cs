using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleWeapon : MonoBehaviour {
    [SerializeField] CollectibleWeaponData data;
    [SerializeField] AudioSource pickupSound;
    [SerializeField] Image TimerImage;
    [SerializeField] float rotationSpeed = 90f;
    float spawnTime;
    bool isCollectible;

    Renderer[] rends;
    Collider coll;

	// Use this for initialization
	void Start () {
        spawnTime = 0f;

        rends = GetComponentsInChildren<Renderer>();
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
        foreach(Renderer rend in rends)
        {
            rend.enabled = false;
        }
        coll.enabled = false;

        spawnTime = 0f;
        isCollectible = false;
    }

    public void Respawn()
    {
        TimerImage.enabled = false;
        foreach (Renderer rend in rends)
        {
            rend.enabled = true;
        }
        coll.enabled = true;

        isCollectible = true;
    }

    private void Rotate(float deltaTime)
    {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if triggered by player
        if (other.gameObject.layer == 10)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.NewWeapon(data.Weapon(), data.Timer());

            pickupSound.Play();

            Collected();
        }
    }
}
