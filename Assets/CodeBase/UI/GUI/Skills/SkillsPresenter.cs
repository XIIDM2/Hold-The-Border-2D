using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class SkillsPresenter : IAsyncStartable, IDisposable
    {
        private SkillsView _view;

        private readonly IUIFactory _factory;
        private readonly SkillRegistry _registry;
        private readonly ISkillService _skillService;
        private readonly CancellationToken _ctc;

        private List<SkillButton> _buttons = new List<SkillButton>();

        public SkillsPresenter(SkillsView view, IUIFactory factory, ISkillService skillService, SkillRegistry registry, CancellationToken ctc)
        {
            _view = view;
            _factory = factory;
            _skillService = skillService;
            _registry = registry;
            _ctc = ctc;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            foreach (SkillData skill in _registry.SkillDatas)
            {
                SkillButton button = await _factory.CreateSkillButton(skill, _ctc);
                button.transform.SetParent(_view.SkillsPanel.transform);

                button.SkillRequested += OnSkillRequested;

                _buttons.Add(button);
            }
        }

        public void Dispose()
        {
            foreach (SkillButton button in _buttons)
            {
                button.SkillRequested -= OnSkillRequested;
            }

            _buttons.Clear();
        }

        private void OnSkillRequested(SkillData skill)
        {
            Debug.Log("Кнопка нажата: " + skill.name);
            _skillService.HandleSkillRequest(skill);
        }
            
    }
}