using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerData : ScriptableObject {
    [SerializeField] float health;
    [SerializeField] float healthMax;
    [SerializeField] float moveSpeed;

    public float Health()
    {
        return health;
    }

    public float HealthMax()
    {
        return healthMax;
    }

    public float MoveSpeed()
    {
        return moveSpeed;
    }
}
