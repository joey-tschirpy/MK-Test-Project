using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] public ProjectileData data;

    void Start()
    {
        Destroy(gameObject, 1f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * data.Speed());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " | destroyed");
        Destroy(gameObject);
    }
}
