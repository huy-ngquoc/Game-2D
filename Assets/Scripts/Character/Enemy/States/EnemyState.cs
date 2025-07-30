#nullable enable

namespace Game;

public abstract class EnemyState : CharacterState
{
    public abstract EnemyGeneralStateMachine EnemyGeneralStateMachine { get; }

    public sealed override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.EnemyGeneralStateMachine;

    public EnemyController EnemyController => this.EnemyGeneralStateMachine.EnemyController;

    public EnemyStats EnemyStats => this.EnemyController.EnemyStats;

    public EnemySkillManager EnemySkillManager => this.EnemyController.EnemySkillManager;

    protected sealed override void OnCharacterStateEnter()
    {
        this.OnEnemyStateEnter();
    }

    protected virtual void OnEnemyStateEnter()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnCharacterStateUpdate()
    {
        this.OnEnemyStateUpdate();
    }

    protected virtual void OnEnemyStateUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected sealed override void OnCharacterStateExit()
    {
        this.OnEnemyStateExit();
    }

    protected virtual void OnEnemyStateExit()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
