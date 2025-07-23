#nullable enable

namespace Game;

public interface ICharacterState
{
    string AnimationBoolName { get; }

    void AnimationFinishTrigger();

    void Enter();

    void Update();

    void Exit();
}
