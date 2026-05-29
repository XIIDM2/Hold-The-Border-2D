using Data;

namespace Infrastructure.Services
{
    public interface ISkillService
    {
        void HandleSkillRequest(SkillData skill);
    }
}