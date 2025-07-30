#nullable enable

namespace Game;

using UnityEngine;

public sealed class EnemyDeadState : EnemyState
{
    public EnemyDeadState(EnemyGeneralStateMachine enemyGeneralStateMachine)
    {
        this.EnemyGeneralStateMachine = enemyGeneralStateMachine;
    }

    public override EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public override string AnimationBoolName => "Die";

    protected override void OnEnemyStateEnter()
    {
        this.EnemyController.Rigidbody2D.linearVelocityX = 0;
        this.EnemyController.gameObject.layer = this.EnemyController.DeadLayerMask;

        this.StateTimer = 2;
    }

    protected override void OnEnemyStateUpdate()
    {
        if (this.StateTimer <= 0)
        {
            Object.Destroy(this.EnemyController.gameObject);
        }
    }

    protected override void OnEnemyStateExit()
    {
        this.EnemyController.gameObject.layer = this.EnemyController.AliveLayerMask;
    }
}
