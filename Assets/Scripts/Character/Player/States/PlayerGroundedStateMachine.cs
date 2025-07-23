#nullable enable

namespace Game;

public sealed class PlayerGroundedStateMachine : PlayerSpecificStateMachine
{
    public PlayerGroundedStateMachine(PlayerGeneralStateMachine playerGeneralStateMachine)
    {
        this.PlayerGeneralStateMachine = playerGeneralStateMachine;
        this.IdleState = new PlayerIdleState(this);
        this.RunState = new PlayerRunState(this);
    }

    public override PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public override ICharacterState InitialState
    {
        get
        {
            var playerInputHandler = this.PlayerInputHandler;
            if (playerInputHandler.MoveInputXInt != 0)
            {
                return this.RunState;
            }
            else
            {
                return this.IdleState;
            }
        }
    }

    public PlayerIdleState IdleState { get; }

    public PlayerRunState RunState { get; }
}
