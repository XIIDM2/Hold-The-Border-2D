using System;
using UnityEngine;

public interface IInputService
{
    void EnableSkillMap();
    void DisableSkillMap();

    void HandleTargeting(Action<Vector2> confirmTarget, Action cancelTarget, Action<Vector2> positionChanged);
}
