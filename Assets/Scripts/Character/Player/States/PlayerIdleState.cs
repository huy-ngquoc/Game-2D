#nullable enable

namespace Game;

using UnityEngine;

public sealed class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerGroundedStateMachine playerGroundedStateMachine)
    {
        this.PlayerGroundedStateMachine = playerGroundedStateMachine;
    }

    public override PlayerGroundedStateMachine PlayerGroundedStateMachine { get; }

    public override string AnimationBoolName => "Idle";

    protected override void OnPlayerGroundedStateEnter()
    {
        this.PlayerController.Rigidbody2D.linearVelocity = Vector2.zero;
    }

    protected override void OnPlayerGroundedStateUpdate()
    {
        var moveInputXInt = this.PlayerInputHandler.MoveInputXInt;
        if (moveInputXInt != 0)
        {
            this.PlayerGroundedStateMachine.SetStateToChangeTo(this.PlayerGroundedStateMachine.RunState);
        }
    }
}
