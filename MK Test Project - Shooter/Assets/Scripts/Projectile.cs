using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] public ProjectileData data;

    private void OnCollisionEnter(Collision collision)
    {
        // Create impact
        string contactMaterial = collision.gameObject.tag;
        switch (contactMaterial)
        {
            case "Flesh":
                Instantiate(data.ParticleImpactPrefabFlesh(),
                    transform.position, transform.rotation);
                break;
            case "Metal":
                Instantiate(data.ParticleImpactPrefabMetal(),
                    transform.position, transform.rotation);
                break;
        }

        // Check if object is killable to take damage, killable layer is 9
        if (collision.gameObject.layer == 9)
        {
            Status status = collision.gameObject.GetComponent<Status>();
            status.TakeDamage(data.Damage());
        }

        // Reset bullet movement and set inactive ready to use again
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
