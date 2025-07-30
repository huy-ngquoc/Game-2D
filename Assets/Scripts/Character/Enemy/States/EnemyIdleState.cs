#nullable enable

namespace Game;

using UnityEngine;

public sealed class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyGeneralStateMachine enemyGeneralStateMachine)
    {
        this.EnemyGeneralStateMachine = enemyGeneralStateMachine;
    }

    public override EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public override string AnimationBoolName => "Idle";

    protected override void OnEnemyStateEnter()
    {
        this.EnemyController.Rigidbody2D.linearVelocity = Vector2.zero;
        this.StateTimer = Random.Range(
            this.EnemyGeneralStateMachine.MinIdleTime,
            this.EnemyGeneralStateMachine.MaxIdleTime);
    }

    protected override void OnEnemyStateUpdate()
    {
        var targetRaycastHit2D = this.EnemyController.TargetRaycastHit2D;
        var isTargetDetected = targetRaycastHit2D.collider != null;
        if (isTargetDetected)
        {
            if (targetRaycastHit2D.distance <= this.EnemySkillManager.AttackSkill.AttackRange)
            {
                this.EnemySkillManager.AttackSkill.Cast();
            }
            else
            {
                this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.RunState);
            }

            return;
        }

        if (this.StateTimer <= 0)
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.RunState);
        }
    }
}
