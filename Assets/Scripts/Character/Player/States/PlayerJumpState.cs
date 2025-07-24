#nullable enable

namespace Game;

public sealed class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Jump";

    protected override void OnPlayerStateEnter()
    {
        this.PlayerInputHandler.CancelJumpInputAction();

        var playerController = this.PlayerController;
        playerController.Rigidbody2D.AddForceY(playerController.JumpForce);
    }

    protected override void OnPlayerStateFixedUpdate()
    {
        var rigidbody2D = this.PlayerController.Rigidbody2D;
        if (rigidbody2D.linearVelocityY <= 0)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.FallState);
            return;
        }

        var moveInputXInt = this.PlayerInputHandler.MoveInputXInt;
        var linearVelocityX = moveInputXInt * this.PlayerController.MoveSpeed * 0.8F;
        rigidbody2D.linearVelocityX = linearVelocityX;
        this.PlayerController.FlipController(linearVelocityX);
    }
}
