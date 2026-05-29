using System;
using UnityEngine.InputSystem;
using VContainer.Unity;
using static InputSystem_Actions;

namespace Infrastructure.Services
{
    public class InputService : IInputService, ISkillTargetingActions, IStartable, IDisposable
    {
        public event Action SkillTargeted;
        public event Action SkillCanceled;

        private InputSystem_Actions _actions;

        public void Start()
        {
            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.SkillTargeting.SetCallbacks(this);
            }
            _actions.Enable();
            DisableSkillMap();
        }

        public void Dispose()
        {
            _actions.Disable();
        }

        public void EnableSkillMap()
        {
            _actions.SkillTargeting.Enable();
        }

        public void DisableSkillMap()
        {
            _actions.SkillTargeting.Disable();
        }

        public void OnSelectTarget(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SkillTargeted?.Invoke();
            }
        }

        public void OnCancelTarget(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SkillCanceled?.Invoke();
            }
        }

    }
}