#nullable enable

namespace Game;

public sealed class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyGeneralStateMachine enemyGeneralStateMachine)
    {
        this.EnemyGeneralStateMachine = enemyGeneralStateMachine;
    }

    public override EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public override string AnimationBoolName => "Idle";
}
