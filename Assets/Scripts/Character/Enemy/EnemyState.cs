#nullable enable

namespace Game;

public abstract class EnemyState : CharacterState
{
    public abstract EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public sealed override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.EnemyGeneralStateMachine;

    public EnemyController EnemyController => this.EnemyGeneralStateMachine.EnemyController;
}
