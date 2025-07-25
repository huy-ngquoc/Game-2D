#nullable enable

namespace Game;

public sealed class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyGeneralStateMachine enemyGeneralStateMachine)
    {
        this.EnemyGeneralStateMachine = enemyGeneralStateMachine;
    }

    public override EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public override string AnimationBoolName => "Attack";

    protected override void OnEnemyStateEnter()
    {
        this.EnemyController.Rigidbody2D.linearVelocityX = 0;
    }

    protected override void OnEnemyStateUpdate()
    {
        if (!this.TriggerCalled)
        {
            return;
        }

        var targetRaycastHit2D = this.EnemyController.TargetRaycastHit2D;

        var isTargetDetected = targetRaycastHit2D.collider != null;
        if (!isTargetDetected)
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.RunState);
            return;
        }

        var isTargetInRange = targetRaycastHit2D.distance <= this.EnemyStats.AttackRange;
        if (!isTargetInRange)
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.RunState);
        }
        else if (!this.EnemySkillManager.AttackSkill.Cast())
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.IdleState);
        }
        else
        {
            // Attack skill casted again.
        }
    }
}
