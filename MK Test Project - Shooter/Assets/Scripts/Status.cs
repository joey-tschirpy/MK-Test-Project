using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    [SerializeField] PlayerData data;
    private float health;

    enum Type { Player, Enemy };
    [SerializeField] Type type;

    // Use this for initialization
    void Start () {
        health = data.HealthMax();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public float Health()
    {
        return health;
    }

    public PlayerData Data()
    {
        return data;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0f)
        {
            switch(type)
            {
                case Type.Player:
                    Debug.Log("you lose");
                    break;
                case Type.Enemy:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
