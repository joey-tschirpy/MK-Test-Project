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

        // Damage object if able to
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("player not hit");
        }
        else
        {
            player.TakeDamage(data.Damage());
        }
        

        Destroy(gameObject);
    }
}
