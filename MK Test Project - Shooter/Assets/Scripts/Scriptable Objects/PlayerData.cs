using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerData : ScriptableObject {
    [SerializeField] float healthMax;

    public float HealthMax()
    {
        return healthMax;
    }
}
