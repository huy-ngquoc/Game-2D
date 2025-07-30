#nullable enable

namespace Game;

public sealed class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override string AnimationBoolName => "Attack";

    protected override void OnPlayerStateEnter()
    {
        this.PlayerController.Rigidbody2D.linearVelocityX = 0;
    }

    protected override void OnPlayerStateUpdate()
    {
        if (this.TriggerCalled)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.GroundState);
        }
    }
}
