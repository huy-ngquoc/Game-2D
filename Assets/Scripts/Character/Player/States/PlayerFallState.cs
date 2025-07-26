#nullable enable

namespace Game;

public sealed class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Fall";

    protected override void OnPlayerStateUpdate()
    {
        if (this.PlayerController.IsGroundDetected)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.GroundState);
        }

        var moveInputXInt = this.PlayerInputHandler.MoveInputXInt;
        var linearVelocityX = moveInputXInt * this.PlayerStats.MoveSpeed * 0.8F;
        this.PlayerController.Rigidbody2D.linearVelocityX = linearVelocityX;
        this.PlayerController.FlipController(linearVelocityX);
    }
}
