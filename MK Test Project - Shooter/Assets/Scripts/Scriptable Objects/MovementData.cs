using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Movement Data")]
public class MovementData : ScriptableObject {
    [Tooltip("1 equates to normal speed")]
    [Range(0, 2)] [SerializeField] float moveSpeed = 1;

    public float MoveSpeed()
    {
        return moveSpeed;
    }
}
