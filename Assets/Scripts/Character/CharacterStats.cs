#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int currentHealth = 1000;

    [SerializeField]
    [Range(5, 40)]
    private float moveSpeed = 8;

    public abstract CharacterController CharacterController { get; }

    public int CurrentHealth => this.currentHealth;

    public float MoveSpeed => this.moveSpeed;
}
