using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerData : ScriptableObject {
    float health;
    [SerializeField] float healthMax;
    [Tooltip("1 equates to normal speed")]
    [Range(0, 2)] [SerializeField] float moveSpeed = 1;

    public void init()
    {
        health = healthMax;
    }

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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0f)
        {
            health = 0f;
        }
    }
}
