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

        this.EnemyController.Rigidbody2D.linearVelocityX = this.EnemyController.MoveSpeed * this.EnemyController.FacingDirection;
    }
}
