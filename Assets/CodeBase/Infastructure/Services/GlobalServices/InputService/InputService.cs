using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class InputService : IInputService, IStartable, IDisposable
    {
        private Action<Vector2> SkillTargeted;
        private Action SkillCanceled;
        private Action<Vector2> SkillPositionChanged;

        private InputSystem_Actions _actions;
        private Camera _camera;

        public void Start()
        {
            _actions ??= new InputSystem_Actions();

            _actions.SkillTargeting.ConfirmTarget.performed += OnConfirmTarget;
            _actions.SkillTargeting.CancelTarget.performed += OnCancelTarget;
            _actions.SkillTargeting.GetTargetPosition.performed += OnGetTargetPosition;
        }

        public void Dispose()
        {
            _actions.SkillTargeting.ConfirmTarget.performed -= OnConfirmTarget;
            _actions.SkillTargeting.CancelTarget.performed -= OnCancelTarget;
            _actions.SkillTargeting.GetTargetPosition.performed -= OnGetTargetPosition;

            _actions.Dispose();
        }

        public void EnableSkillMap()
        {
            _camera = Camera.main;
            _actions.SkillTargeting.Enable();
        }

        public void DisableSkillMap()
        {
            _camera = null;

            SkillTargeted = null;
            SkillCanceled = null;
            SkillPositionChanged = null;

            _actions.SkillTargeting.Disable();
        }

        public void HandleTargeting(Action<Vector2> confirmTarget, Action cancelTarget, Action<Vector2> positionChanged)
        {

            SkillTargeted = confirmTarget;
            SkillCanceled = cancelTarget;
            SkillPositionChanged = positionChanged;

            EnableSkillMap();
        }

        private void OnConfirmTarget(InputAction.CallbackContext context)
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(_actions.SkillTargeting.GetTargetPosition.ReadValue<Vector2>());
            SkillTargeted?.Invoke(worldPos);
            DisableSkillMap();
        }

        private void OnCancelTarget(InputAction.CallbackContext context)
        {
            SkillCanceled?.Invoke();
            DisableSkillMap();
        }

        private void OnGetTargetPosition(InputAction.CallbackContext context)
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            SkillPositionChanged?.Invoke(worldPos);
        }

    }
}