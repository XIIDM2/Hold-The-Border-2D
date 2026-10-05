using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using Infastructure.Services;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class SkillService : ISkillService, ITickable
    {
        public event UnityAction<SkillData> SkillApplied;
        private bool isTargeting = false;
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

        public void Tick()
        {
            if (!isTargeting) return;

            Vector2 position = Camera.main.ScreenToWorldPoint(_inputService.PointerPosition);

            _visualizerService.SetVisualizerPosition(position);
        }

        public void HandleSkillRequest(SkillData skill)
        {
            _currentSkill = skill;

            if (_currentSkill.CastType == SkillCastType.InstantCast)
            {
                ExecuteSkill();
            }
            else if (_currentSkill.CastType == SkillCastType.TargetCast)
            {
                Cursor.visible = false;

                _visualizerService.SetVisualizerRadius(skill.Radius);
                _visualizerService.SetVisualizerHologram(skill.Icon);
                _visualizerService.ShowVisualizer();

                _inputService.SkillTargeted += OnSkillTargeted;
                _inputService.SkillCanceled += OnSkillCancelled;

                _inputService.EnableSkillMap();

                isTargeting = true;

            }
        }

        private void ExecuteSkill(Vector2? position = null)
        {
            if (_player.Gold >= _currentSkill.Price)
            {
                _currentSkill.Execute(position).Forget();
                _player.TrySpendGold(_currentSkill.Price);
                SkillApplied.Invoke(_currentSkill);
            }
        }

        private void OnSkillTargeted(Vector2 position)
        {
            if (_currentSkill == null) return;
            ExecuteSkill(position);

            CleanUp();
        }

        private void OnSkillCancelled()
        {
            CleanUp();
        }


        private void CleanUp()
        {
            _currentSkill = null;
            isTargeting = false;

            _inputService.SkillTargeted -= OnSkillTargeted;
            _inputService.SkillCanceled -= OnSkillCancelled;

            Cursor.visible = true;

            _visualizerService.HideVisualizer();
        }
    }
}