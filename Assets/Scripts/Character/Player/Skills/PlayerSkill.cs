#nullable enable

namespace Game;

using UnityEngine;

public abstract class PlayerSkill : CharacterSkill
{
    [SerializeField]
    [ResolveComponent]
    private PlayerSkillManager playerSkillManager = null!;

    public PlayerSkillManager PlayerSkillManager => this.playerSkillManager;

    public sealed override CharacterSkillManager CharacterSkillManager => this.playerSkillManager;

    public PlayerController PlayerController => this.playerSkillManager.PlayerController;

    public PlayerGeneralStateMachine PlayerGeneralStateMachine => this.PlayerController.PlayerGeneralStateMachine;
}
