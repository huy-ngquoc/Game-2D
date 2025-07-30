#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterAnimationTrigger : MonoBehaviour
{
    public abstract CharacterController CharacterController { get; }

    private void AnimationFinishTrigger() => this.CharacterController.AnimationFinishTrigger();

    private void AnimationAttackTrigger()
    {
        var colliders = Physics2D.OverlapCircleAll(this.CharacterController.AttackCheckPosition, this.CharacterController.AttackCheckRadius, this.CharacterController.AttackTargetLayerMask);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<CharacterStats>(out var targetStats))
            {
                targetStats.TakeDamage(this.CharacterController.CharacterSkillManager.CharacterAttackSkill.Damage);
            }
        }
    }
}
