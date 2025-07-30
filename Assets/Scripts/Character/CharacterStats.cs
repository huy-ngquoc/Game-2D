#nullable enable

namespace Game;

using System;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [SerializeReference]
    [RequireReference]
    private CombatTextUIController combatTextPrefab = null!;

    [SerializeField]
    private int baseHealth = 1000;

    private int currentHealth = 1000;

    [SerializeField]
    [Range(5, 40)]
    private float moveSpeed = 8;

    public event EventHandler HealthChanged
    {
        add => this.OnHealthChanged += value;
        remove => this.OnHealthChanged -= value;
    }

    private event EventHandler? OnHealthChanged = null;

    public abstract CharacterController CharacterController { get; }

    public int BaseHealth => this.baseHealth;

    public int CurrentHealth => this.currentHealth;

    public bool IsAlive => this.currentHealth > 0;

    public float MoveSpeed => this.moveSpeed;

    public void Setup()
    {
        this.currentHealth = this.baseHealth;
        this.OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(int damage)
    {
        if (this.currentHealth <= 0)
        {
            return;
        }

        if (this.currentHealth <= damage)
        {
            this.currentHealth = 0;
            this.OnTakeDamage(damage);

            this.CharacterController.Die();
            return;
        }

        this.currentHealth -= damage;
        this.OnTakeDamage(damage);
    }

    protected virtual void OnCharacterStatsSetup()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    private void OnTakeDamage(int damage)
    {
        this.OnHealthChanged?.Invoke(this, EventArgs.Empty);

        var go = UnityEngine.Object.Instantiate(this.combatTextPrefab, this.transform.position + Vector3.up, Quaternion.identity);
        go.Setup(Camera.main, damage);
    }
}
