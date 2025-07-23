#nullable enable

namespace Game;

public abstract class CharacterSpecificStateMachine : ICharacterState
{
    private ICharacterState currentState = null!;
    private ICharacterState? stateToChangeTo = null;

    public string AnimationBoolName => this.currentState.AnimationBoolName;

    public bool HasStateToChangeTo => this.stateToChangeTo != null;

    public abstract CharacterGeneralStateMachine CharacterGeneralStateMachine { get; }

    public CharacterController CharacterController => this.CharacterGeneralStateMachine.CharacterController;

    public abstract ICharacterState InitialState { get; }

    public void SetStateToChangeTo(ICharacterState newState)
    {
        this.stateToChangeTo = newState;
    }

    public void CancelChangingState()
    {
        this.stateToChangeTo = null;
    }

    public void AnimationFinishTrigger() => this.currentState.AnimationFinishTrigger();

    public void Enter()
    {
        this.currentState = this.InitialState;
        this.currentState.Enter();
    }

    public void Update()
    {
        if (this.stateToChangeTo == null)
        {
            this.currentState.Update();
            return;
        }

        do
        {
            this.currentState.Exit();
            this.currentState = this.stateToChangeTo;
            this.stateToChangeTo = null;
            this.currentState.Enter();
        }
        while (this.stateToChangeTo != null);
    }

    public void Exit()
    {
        this.currentState.Exit();
    }
}
