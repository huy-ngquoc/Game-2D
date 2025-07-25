#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterSkill : MonoBehaviour
{
    [SerializeField]
    [Range(0, 300)]
    private float cooldown = 0;

    private float cooldownTimer = 0;

    public abstract CharacterSkillManager CharacterSkillManager { get; }

    public CharacterController CharacterController => this.CharacterSkillManager.CharacterController;

    public bool IsUsable => this.cooldownTimer <= 0;

    public bool Cast()
    {
        if (!this.IsUsable)
        {
            return false;
        }

        this.CastLogic();
        this.cooldownTimer = this.cooldown;
        return true;
    }

    protected abstract void CastLogic();

    protected void Awake()
    {
        this.OnCharacterSkillAwake();
    }

    protected virtual void OnCharacterSkillAwake()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }

    protected void Update()
    {
        var deltaTime = Time.deltaTime;
        if (this.cooldownTimer > deltaTime)
        {
            this.cooldownTimer -= deltaTime;
        }
        else
        {
            this.cooldownTimer = 0;
        }

        this.OnCharacterSkillUpdate();
    }

    protected virtual void OnCharacterSkillUpdate()
    {
        // Leave this method blank
        // The derived classes can decide if they override this method
    }
}
