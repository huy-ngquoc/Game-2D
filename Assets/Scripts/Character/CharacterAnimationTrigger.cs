#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterAnimationTrigger : MonoBehaviour
{
    public abstract CharacterController CharacterController { get; }

    private void AnimationFinishTrigger() => this.CharacterController.AnimationFinishTrigger();
}
