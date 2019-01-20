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
        string contactMaterial = collision.gameObject.tag;

        switch (contactMaterial)
        {
            case "Metal":
                Debug.Log("hit metal");
                GameObject impact = Instantiate(data.ParticleImpactPrefabMetal(),
                    transform.position, transform.rotation);
                break;
        }

        Destroy(gameObject);
    }
}
