using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject {
    [SerializeField] float damage;
    [SerializeField] GameObject particleImpactPrefabFlesh;
    [SerializeField] GameObject particleImpactPrefabMetal;
    [SerializeField] float speed;

    public float Damage()
    {
        return damage;
    }

    public GameObject ParticleImpactPrefabFlesh()
    {
        return particleImpactPrefabFlesh;
    }

    public GameObject ParticleImpactPrefabMetal()
    {
        return particleImpactPrefabMetal;
    }

    public float Speed()
    {
        return speed;
    }
}
