#nullable enable

namespace Game;

public abstract class PlayerGroundedState : PlayerState
{
    public abstract PlayerGroundedStateMachine PlayerGroundedStateMachine { get; }

    public sealed override PlayerGeneralStateMachine PlayerGeneralStateMachine
        => this.PlayerGroundedStateMachine.PlayerGeneralStateMachine;

    protected sealed override void OnPlayerStateEnter()
    {
        this.OnPlayerGroundedStateEnter();
    }

    protected virtual void OnPlayerGroundedStateEnter()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnPlayerStateUpdate()
    {
        if (!this.PlayerController.IsGroundDetected)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.FallState);
            return;
        }

        if (this.PlayerInputHandler.JumpPressed)
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.JumpState);
            return;
        }

        if (this.PlayerInputHandler.ThrowPressed && this.PlayerSkillManager.ThrowSkill.Cast())
        {
            return;
        }

        this.OnPlayerGroundedStateUpdate();
    }

    protected virtual void OnPlayerGroundedStateUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnPlayerStateExit()
    {
        this.OnPlayerGroundedStateExit();
    }

    protected virtual void OnPlayerGroundedStateExit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
