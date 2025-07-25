#nullable enable

namespace Game;

public abstract class PlayerSkill : CharacterSkill
{
    public abstract PlayerSkillManager PlayerSkillManager { get; }
}
