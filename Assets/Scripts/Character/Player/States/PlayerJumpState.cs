#nullable enable

namespace Game;

public sealed class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public int JumpLeft { get; set; } = 1;

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Jump";

    protected override void OnPlayerStateEnter()
    {
        this.PlayerInputHandler.CancelJumpInputAction();
        this.PlayerController.Rigidbody2D.AddForceY(this.PlayerStats.JumpForce);
        --this.PlayerGeneralStateMachine.JumpState.JumpLeft;
    }

    protected override void OnPlayerStateUpdate()
    {
        if (this.PlayerInputHandler.JumpPressed && (this.PlayerGeneralStateMachine.JumpState.JumpLeft > 0))
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.JumpState);
            return;
        }

        var rigidbody2D = this.PlayerController.Rigidbody2D;
        if (rigidbody2D.linearVelocityY <= 0)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.FallState);
            return;
        }

        var moveInputXInt = this.PlayerInputHandler.MoveInputXInt;
        var linearVelocityX = moveInputXInt * this.PlayerStats.MoveSpeed * 0.8F;
        rigidbody2D.linearVelocityX = linearVelocityX;
        this.PlayerController.FlipController(linearVelocityX);
    }
}
