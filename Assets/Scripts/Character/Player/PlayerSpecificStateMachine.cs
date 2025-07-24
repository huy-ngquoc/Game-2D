#nullable enable

namespace Game;

public abstract class PlayerSpecificStateMachine : CharacterSpecificStateMachine
{
    public abstract PlayerGeneralStateMachine PlayerGeneralStateMachine { get; }

    public PlayerController PlayerController => this.PlayerGeneralStateMachine.PlayerController;

    public PlayerStats PlayerStats => this.PlayerController.PlayerStats;

    public PlayerInputHandler PlayerInputHandler => this.PlayerController.InputHandler;

    public sealed override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.PlayerGeneralStateMachine;
}
