using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        public event UnityAction<SkillData> SkillApplied;
        private SkillData _currentSkill;

        private readonly IInputService _inputService;
        private readonly IVisualizerService _visualizerService;
        private readonly IPlayerController _player;

        public SkillService(IInputService inputService, IVisualizerService visualizerService, IPlayerController player)
        {
            _inputService = inputService;
            _visualizerService = visualizerService;
            _player = player;
        }

        public void HandleSkillRequest(SkillData skill)
        {
            _currentSkill = skill;

            if (_currentSkill.CastType == SkillCastType.InstantCast)
            {
                _currentSkill.Execute().Forget();
                if (_player.Gold >= _currentSkill.Price)
                {
                    _player.TrySpendGold(_currentSkill.Price);
                    SkillApplied.Invoke(_currentSkill);
                }
                
            }
            else if (_currentSkill.CastType == SkillCastType.TargetCast)
            {
                Cursor.visible = false;

                _visualizerService.SetVisualizerRadius(skill.Radius);
                _visualizerService.SetVisualizerHologram(skill.Icon);
                _visualizerService.ShowVisualizer();

                _inputService.HandleTargeting(OnSkillTargeted, OnSkillCancelled, OnPositonChanged);


            }
        }

        private void OnSkillTargeted(Vector2 position)
        {
            if (_currentSkill == null) return;

            _currentSkill.Execute(position).Forget();
            if (_player.Gold >= _currentSkill.Price)
            {
                _player.TrySpendGold(_currentSkill.Price);
                SkillApplied.Invoke(_currentSkill);
            }

            CleanUp();
        }

        private void OnSkillCancelled()
        {
            CleanUp();
        }

        private void OnPositonChanged(Vector2 position)
        {
            _visualizerService.SetVisualizerPosition(position);
        }

        private void CleanUp()
        {
            _currentSkill = null;
            Cursor.visible = true;
            _visualizerService.HideVisualizer();
        }
    }
}