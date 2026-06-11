using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Events;
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

        private readonly ISkillService _skillService;

        private readonly IEventBus _eventBus;

        private readonly SkillRegistry _registry;
        private readonly CancellationToken _ctc;

        private List<SkillButton> _buttons = new List<SkillButton>();

        public SkillsPresenter(SkillsView view, IUIFactory factory, IEventBus eventBus, ISkillService skillService, SkillRegistry registry, CancellationToken ctc)
        {
            _view = view;
            _factory = factory;
            _eventBus = eventBus;
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

            _eventBus.Subscribe<UIStateChanged>(OnUIStateChanged);
        }

        public void Dispose()
        {
            foreach (SkillButton button in _buttons)
            {
                button.SkillRequested -= OnSkillRequested;
            }

            _buttons.Clear();

            _eventBus.Unsubscribe<UIStateChanged>(OnUIStateChanged);
        }

        private void OnSkillRequested(SkillData skill)
        {
            _skillService.HandleSkillRequest(skill);
        }

        private void OnUIStateChanged(UIStateChanged state) 
        {
            if (state.CurrentState == UIStates.InTowerBuildingPanel || state.CurrentState == UIStates.InPausePanel)
            {
                _view.HidePanel();
            }
            else if (state.CurrentState == UIStates.InActiveGameplay)
            {
                _view.ShowPanel();
            }
        }
            
    }
}