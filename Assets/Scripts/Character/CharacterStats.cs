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

    [SerializeField]
    [Range(0.5F, 5)]
    private float attackRange = 5;

    public abstract CharacterController CharacterController { get; }

    public int CurrentHealth => this.currentHealth;

    public float MoveSpeed => this.moveSpeed;

    public float AttackRange => this.attackRange;

    public void TakeDamage(int damage)
    {
        if (this.currentHealth < damage)
        {
            this.currentHealth = 0;
            this.CharacterController.Die();
            return;
        }

        this.currentHealth -= damage;
    }
}
