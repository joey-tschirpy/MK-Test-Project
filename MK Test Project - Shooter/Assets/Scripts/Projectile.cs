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

        // Check if player hit
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null)
        {
            // Check if enemy hit
        }
        else
        {
            player.TakeDamage(data.Damage());
        }

        // Reset bullet movement and set inactive ready to use again
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
