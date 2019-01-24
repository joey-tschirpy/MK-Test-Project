using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_KILLALL : MonoBehaviour {
    [SerializeField] GameObject enemies;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < enemies.transform.childCount; i++)
            {
                Destroy(enemies.transform.GetChild(i).gameObject);
            }
        }
	}
}
