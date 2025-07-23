#nullable enable

namespace Game;

public sealed class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerGroundedStateMachine playerGroundedStateMachine)
    {
        this.PlayerGroundedStateMachine = playerGroundedStateMachine;
    }

    public override PlayerGroundedStateMachine PlayerGroundedStateMachine { get; }

    public override string AnimationBoolName => "Run";

    protected override void OnPlayerGroundedStateUpdate()
    {
        var moveInputXInt = this.PlayerInputHandler.MoveInputXInt;
        if (moveInputXInt == 0)
        {
            this.PlayerGroundedStateMachine.SetStateToChangeTo(this.PlayerGroundedStateMachine.IdleState);
            return;
        }

        var moveSpeedX = this.PlayerInputHandler.MoveInputX * this.PlayerController.MoveSpeed;
        var linearVelocityX = moveSpeedX + this.PlayerController.PlatformVelocity.x;
        this.PlayerController.Rigidbody2D.linearVelocityX = linearVelocityX;
        this.PlayerController.FlipController(moveSpeedX);
    }
}
