#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int baseHealth = 1000;

    private int currentHealth = 1000;

    [SerializeField]
    [Range(5, 40)]
    private float moveSpeed = 8;

    public abstract CharacterController CharacterController { get; }

    public int CurrentHealth => this.currentHealth;

    public bool IsAlive => this.currentHealth > 0;

    public float MoveSpeed => this.moveSpeed;

    public void Setup()
    {
        this.currentHealth = this.baseHealth;
    }

    public void TakeDamage(int damage)
    {
        if (this.currentHealth <= 0)
        {
            return;
        }

        if (this.currentHealth < damage)
        {
            this.currentHealth = 0;
            this.CharacterController.Die();
            return;
        }

        this.currentHealth -= damage;
    }

    protected virtual void OnCharacterStatsSetup()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
