#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterState : ICharacterState
{
    public abstract string AnimationBoolName { get; }

    public float StateTimer { get; protected set; } = 0;

    public bool TriggerCalled { get; protected set; } = false;

    public abstract CharacterGeneralStateMachine CharacterGeneralStateMachine { get; }

    public CharacterController CharacterController => this.CharacterGeneralStateMachine.CharacterController;

    public void AnimationFinishTrigger() => this.TriggerCalled = true;

    public void Enter()
    {
        this.TriggerCalled = false;

        var animationBoolName = this.AnimationBoolName;
        if (!string.IsNullOrWhiteSpace(animationBoolName))
        {
            this.CharacterController.Animator.SetTrigger(animationBoolName);
        }

        this.OnCharacterStateEnter();
    }

    public void Update()
    {
        if (this.StateTimer > Time.deltaTime)
        {
            this.StateTimer -= Time.deltaTime;
        }
        else
        {
            this.StateTimer = 0;
        }

        this.OnCharacterStateUpdate();
    }

    public void Exit()
    {
        var animationBoolName = this.AnimationBoolName;
        if (!string.IsNullOrWhiteSpace(animationBoolName))
        {
            this.CharacterController.Animator.ResetTrigger(animationBoolName);
        }

        this.OnCharacterStateExit();
    }

    protected virtual void OnCharacterStateEnter()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected virtual void OnCharacterStateUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected virtual void OnCharacterStateExit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
