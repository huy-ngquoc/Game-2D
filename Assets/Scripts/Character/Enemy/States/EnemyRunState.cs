#nullable enable

namespace Game;

public sealed class EnemyRunState : EnemyState
{
    public EnemyRunState(EnemyGeneralStateMachine enemyGeneralStateMachine)
    {
        this.EnemyGeneralStateMachine = enemyGeneralStateMachine;
    }

    public override string AnimationBoolName => "Run";

    public override EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    protected override void OnEnemyStateFixedUpdate()
    {
        if (!this.EnemyController.IsGroundDetected)
        {
            this.EnemyController.Flip();
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.IdleState);
            return;
        }

        var targetRaycastHit2D = this.EnemyController.TargetRaycastHit2D;
        var isTargetDetected = targetRaycastHit2D.collider != null;
        if (isTargetDetected
            && (targetRaycastHit2D.distance <= this.EnemyStats.AttackRange))
        {
            if (!this.EnemySkillManager.AttackSkill.Cast())
            {
                this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.IdleState);
            }
            return;
        }

        this.EnemyController.Rigidbody2D.linearVelocityX = this.EnemyStats.MoveSpeed * this.EnemyController.FacingDirection;
    }
}
