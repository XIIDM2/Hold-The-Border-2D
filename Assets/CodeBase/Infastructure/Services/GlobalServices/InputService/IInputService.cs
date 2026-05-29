using System;

public interface IInputService
{
    event Action SkillTargeted;
    event Action SkillCanceled;

    void EnableSkillMap();
    void DisableSkillMap();
}
