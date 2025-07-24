#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterGeneralStateMachine : MonoBehaviour
{
    private ICharacterState currentState = null!;
    private ICharacterState? stateToChangeTo = null;
    private ICharacterState lastState = null!;

    public string LastAnimationBoolName => this.lastState.AnimationBoolName;

    public bool HasStateToChangeTo => this.stateToChangeTo != null;

    public abstract CharacterController CharacterController { get; }

    public abstract ICharacterState InitialState { get; }

    public void SetStateToChangeTo(ICharacterState newState)
    {
        this.stateToChangeTo = newState;
    }

    public void SetStateToLastState()
    {
        this.stateToChangeTo = this.lastState;
    }

    public void CancelChangingState()
    {
        this.stateToChangeTo = null;
    }

    public void AnimationFinishTrigger() => this.currentState.AnimationFinishTrigger();

    protected void Awake()
    {
        this.lastState = this.InitialState;
        this.currentState = this.InitialState;
        this.currentState.Enter();
    }

    protected void Update()
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

    protected void FixedUpdate()
    {
        if (this.stateToChangeTo == null)
        {
            this.currentState.FixedUpdate();
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
}
