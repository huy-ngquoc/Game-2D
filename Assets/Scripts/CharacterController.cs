#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [SerializeReference]
    [ResolveComponent]
    private new Rigidbody2D rigidbody2D = null!;

    [SerializeReference]
    [ResolveComponent]
    private CapsuleCollider2D capsuleCollider2D = null!;

    [SerializeReference]
    [ResolveComponentInChildren]
    private Animator animator = null!;

    private string currentAnimationName = "Idle";

    private bool isFacingRight = true;

    [Header("Character Collision info")]

    [SerializeReference]
    [ResolveComponentInChildren("Ground Check")]
    private Transform groundCheck = null!;

    [SerializeField]
    [LayerMaskIsNothingOrEverythingWarning]
    private LayerMask groundLayerMask = new();

    [SerializeField]
    [Range(0.01F, 2)]
    private float groundCheckDistance = 0.1F;

    [Header("Character Stats")]

    [field: SerializeField]
    [field: Range(5, 40)]
    private float moveSpeed = 8;

    private float currentHealth = 100;

    protected CharacterController()
    {
    }

    public bool IsGroundDetected => Physics2D.Raycast(
        this.groundCheck.position,
        Vector2.down,
        this.groundCheckDistance,
        this.groundLayerMask);

    public bool IsFacingRight => this.isFacingRight;

    public int FacingDirection => this.isFacingRight ? 1 : -1;

    public float CurrentHealth => this.currentHealth;

    public bool IsAlive => this.currentHealth > 0;

    public bool IsDead => this.currentHealth <= 0;

    public LayerMask GroundLayerMask => this.groundLayerMask;

    public float MoveSpeed => this.moveSpeed;

    protected Rigidbody2D Rigidbody2D => this.rigidbody2D;

    protected CapsuleCollider2D CapsuleCollider2D => this.capsuleCollider2D;

    public void Init()
    {
        this.currentHealth = 100;

        this.OnCharacterControllerInit();
    }

    public void Despawn()
    {
        this.OnCharacterControllerDespawn();
    }

    public void Hit(float damage)
    {
        if (this.currentHealth <= 0)
        {
            // Dead already...
            return;
        }

        if (this.currentHealth > damage)
        {
            this.currentHealth -= damage;
            return;
        }

        this.currentHealth = 0;
        this.Die();
    }

    protected virtual void OnCharacterControllerInit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected virtual void OnCharacterControllerDespawn()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected void ChangeAnimation(string newAnimationName)
    {
        if (this.currentAnimationName != newAnimationName)
        {
            this.animator.ResetTrigger(this.currentAnimationName);
            this.currentAnimationName = newAnimationName;
            this.animator.SetTrigger(this.currentAnimationName);
        }
    }

    protected void FlipController(float x)
    {
        bool flip = this.IsFacingRight ? (x < 0) : (x > 0);
        if (flip)
        {
            this.Flip();
        }
    }

    protected void Awake()
    {
        this.OnCharacterControllerAwake();
    }

    protected virtual void OnCharacterControllerAwake()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected void FixedUpdate()
    {
        this.OnCharacterControllerFixedUpdate();
    }

    protected virtual void OnCharacterControllerFixedUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.groundCheck.position, new Vector2(this.groundCheck.position.x, this.groundCheck.position.y - this.groundCheckDistance));
    }

    protected virtual void OnCharacterControllerDrawGizmos()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected virtual void OnCharacterControllerDie()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    private void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        this.transform.Rotate(0, 180, 0);
    }

    private void Die()
    {
        this.OnCharacterControllerDie();
    }
}
