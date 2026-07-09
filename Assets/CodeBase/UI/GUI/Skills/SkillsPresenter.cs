using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
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
        private readonly IPlayerController _player;

        private readonly IEventBus _eventBus;

        private readonly SkillRegistry _registry;
        private readonly CancellationToken _ctc;

        private Dictionary<SkillData, SkillButton> _buttons = new();

        public SkillsPresenter(SkillsView view, IUIFactory factory, IEventBus eventBus, ISkillService skillService, IPlayerController player, SkillRegistry registry, CancellationToken ctc)
        {
            _view = view;
            _factory = factory;
            _eventBus = eventBus;
            _skillService = skillService;
            _player = player;
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

                _buttons.TryAdd(skill, button);
            }

            _skillService.SkillApplied += OnSkillApplied;
            _player.GoldChanged += OnGoldChanged;
            _eventBus.Subscribe<UIStateChanged>(OnUIStateChanged);
        }

        public void Dispose()
        {
            foreach (SkillButton button in _buttons.Values)
            {
                button.SkillRequested -= OnSkillRequested;
            }

            _buttons.Clear();

            _skillService.SkillApplied -= OnSkillApplied;
            _player.GoldChanged -= OnGoldChanged;
            _eventBus.Unsubscribe<UIStateChanged>(OnUIStateChanged);
        }

        private void OnSkillRequested(SkillData skill)
        {
            _skillService.HandleSkillRequest(skill);
        }

        private void OnSkillApplied(SkillData skill)
        {
            if (_buttons.TryGetValue(skill, out var button))
            {
                button.ShowCooldown();
            }
        }

        private void OnGoldChanged(int currentAmount)
        {
            foreach (KeyValuePair<SkillData, SkillButton> pair in _buttons)
            {
                pair.Value.SetAffordable(currentAmount >= pair.Key.Price);
            }
        }

        private void OnUIStateChanged(UIStateChanged state) 
        {
            if (state.CurrentState == UIStates.InActiveGameplay)
            {
                _view.ShowPanel();
            }
            else
            {
                _view.HidePanel();
            }
        }
            
    }
}