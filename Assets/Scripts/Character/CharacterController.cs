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

    [SerializeField]
    [LayerMaskIsNothingOrEverythingWarning]
    private LayerMask attackTargetLayerMask = new();

    protected CharacterController()
    {
    }

    public Rigidbody2D Rigidbody2D => this.rigidbody2D;

    public CapsuleCollider2D CapsuleCollider2D => this.capsuleCollider2D;

    public Animator Animator => this.animator;

    public bool IsGroundDetected => Physics2D.Raycast(
        this.groundCheck.position,
        Vector2.down,
        this.groundCheckDistance,
        this.groundLayerMask);

    public bool IsFacingRight => this.isFacingRight;

    public int FacingDirection => this.isFacingRight ? 1 : -1;

    public LayerMask GroundLayerMask => this.groundLayerMask;

    public LayerMask AttackTargetLayerMask => this.attackTargetLayerMask;

    public abstract CharacterGeneralStateMachine CharacterGeneralStateMachine { get; }

    public abstract CharacterStats CharacterStats { get; }

    public abstract CharacterSkillManager CharacterSkillManager { get; }

    public void AnimationFinishTrigger() => this.CharacterGeneralStateMachine.AnimationFinishTrigger();

    public void Init()
    {
        this.OnCharacterControllerInit();
    }

    public void FlipController(float x)
    {
        bool flip = this.IsFacingRight ? (x < 0) : (x > 0);
        if (flip)
        {
            this.Flip();
        }
    }

    public void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        this.transform.Rotate(0, 180, 0);
    }

    protected virtual void OnCharacterControllerInit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
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

    protected void Update()
    {
        this.OnCharacterControllerUpdate();
    }

    protected virtual void OnCharacterControllerUpdate()
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
}
