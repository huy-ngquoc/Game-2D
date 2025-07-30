#nullable enable

namespace Game;

public sealed class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Die";

    protected override void OnPlayerStateEnter()
    {
        this.PlayerController.Rigidbody2D.linearVelocityX = 0;
        this.PlayerController.gameObject.layer = this.PlayerController.DeadLayerMask;

        this.StateTimer = 2;
    }

    protected override void OnPlayerStateUpdate()
    {
        if (this.StateTimer <= 0)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.GroundState);
            this.PlayerController.Setup();
        }
    }

    protected override void OnPlayerStateExit()
    {
        this.PlayerController.gameObject.layer = this.PlayerController.AliveLayerMask;
    }
}
