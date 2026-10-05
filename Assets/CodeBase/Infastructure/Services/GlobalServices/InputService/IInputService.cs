using System;
using UnityEngine;

namespace Infastructure.Services
{
    public interface IInputService
    {
        event Action<Vector2> SkillTargeted;
        event Action SkillCanceled;
        Vector2 PointerPosition { get; }
        void EnableSkillMap();
        void DisableSkillMap();
    }
}