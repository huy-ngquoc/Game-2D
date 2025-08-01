#nullable enable

namespace Game;

public sealed class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Run";

    public float DashSpeed => this.PlayerSkillManager.DashSkill.DashSpeed;

    public float DashDuration => this.PlayerSkillManager.DashSkill.DashDuration;

    public int DashDirection
    {
        get
        {
            var result = this.PlayerController.FacingDirection;

            var moveInputX = this.PlayerInputHandler.MoveInputX;
            if (moveInputX > 0)
            {
                result = 1;
            }
            else if (moveInputX < 0)
            {
                result = -1;
            }
            else
            {
                // Do nothing...
            }

            return result;
        }
    }

    protected override void OnPlayerStateEnter()
    {
        this.PlayerInputHandler.CancelDashInputAction();
        this.StateTimer = this.DashDuration;
    }

    protected override void OnPlayerStateUpdate()
    {
        if (this.StateTimer > 0)
        {
            this.PlayerController.Rigidbody2D.linearVelocityX = this.DashSpeed * this.DashDirection;
            return;
        }

        if (!this.PlayerController.IsGroundDetected)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.FallState);
            return;
        }

        this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.GroundState);
    }

    protected override void OnPlayerStateExit()
    {
        this.PlayerController.Rigidbody2D.linearVelocityX = 0;
    }
}
