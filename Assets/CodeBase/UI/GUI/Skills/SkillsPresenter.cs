using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
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
        private readonly CancellationToken _ctc;

        private List<SkillButton> _buttons = new List<SkillButton>();

        public SkillsPresenter(SkillsView view, IUIFactory factory, SkillRegistry registry, CancellationToken ctc)
        {
            _view = view;
            _factory = factory;
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
                _buttons.Remove(button);
            }
        }

        private void OnSkillRequested(SkillData skill)
        {
            Debug.Log("Starting Executing skill: " + skill.Name);
            // Skill Sevice SkillExecute method invoke
        }
            
    }
}