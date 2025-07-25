#nullable enable

namespace Game;

using UnityEngine;

public abstract class EnemySkill : CharacterSkill
{
    [SerializeReference]
    [ResolveComponent]
    private EnemySkillManager enemySkillManager = null!;

    public EnemySkillManager EnemySkillManager => this.enemySkillManager;

    public sealed override CharacterSkillManager CharacterSkillManager => this.enemySkillManager;

    public EnemyController EnemyController => this.EnemySkillManager.EnemyController;

    public EnemyGeneralStateMachine EnemyGeneralStateMachine => this.EnemyController.EnemyGeneralStateMachine;
}
