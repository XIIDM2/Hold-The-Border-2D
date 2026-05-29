using Cysharp.Threading.Tasks;
using Data;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly IInputService _inputService;

        public SkillService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void HandleSkillRequest(SkillData skill)
        {
            if (skill.CastType == SkillCastType.InstantCast)
            {
                skill.Execute().Forget();
            }
            else if (skill.CastType == SkillCastType.TargetCast)
            {
                _inputService.EnableSkillMap();

                _inputService.SkillTargeted += OnSkillTargeted;
                _inputService.SkillCanceled += OnSkillCanceled;

                void OnSkillTargeted()
                {
                    skill.Execute().Forget();

                    Unsubscribe();
                }

                void OnSkillCanceled()
                {
                    Unsubscribe();
                }

                void Unsubscribe()
                {
                    _inputService.SkillTargeted -= OnSkillTargeted;
                    _inputService.SkillTargeted -= OnSkillTargeted;

                    _inputService.DisableSkillMap();
                }

            }
        }


    }
}