#nullable enable

namespace Game;

using UnityEngine;

public abstract class CharacterSkillManager : MonoBehaviour
{
    public abstract CharacterController CharacterController { get; }
}
