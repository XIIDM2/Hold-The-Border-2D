using Data;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public interface ISkillService
    {
        event UnityAction<SkillData> SkillApplied;
        void HandleSkillRequest(SkillData skill);
    }
}