using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField] private Camera cam;

    // Use this for initialization
    void Start()
    {
        transform.rotation = cam.transform.rotation;
    }

    // Update is called once per frame
    void Update ()
    {

	}
}
