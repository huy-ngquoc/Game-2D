#nullable enable

namespace Game;

public abstract class PlayerState : CharacterState
{
    public abstract PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public sealed override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.PlayerGeneralStateMachine;

    public PlayerController PlayerController => this.PlayerGeneralStateMachine.PlayerController;

    public PlayerStats PlayerStats => this.PlayerController.PlayerStats;

    public PlayerInputHandler PlayerInputHandler => this.PlayerGeneralStateMachine.PlayerController.InputHandler;

    protected sealed override void OnCharacterStateEnter()
    {
        this.OnPlayerStateEnter();
    }

    protected virtual void OnPlayerStateEnter()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnCharacterStateUpdate()
    {
        this.OnPlayerStateUpdate();
    }

    protected virtual void OnPlayerStateUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnCharacterStateExit()
    {
        this.OnPlayerStateExit();
    }

    protected virtual void OnPlayerStateExit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
