using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Collectible Data")]
public class CollectibleData : ScriptableObject {
    public enum Type { Health, Armour}

    [SerializeField] float respawnTime;
    [SerializeField] Type statType;
    [SerializeField] float value;

    public float RespawnTime()
    {
        return respawnTime;
    }

    public Type StatType()
    {
        return statType;
    }

    public float Value()
    {
        return value;
    }
}
