using Infastructure.Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class InputService : IInputService, IStartable, IDisposable
    {
        public Vector2 PointerPosition => Pointer.current != null ? Pointer.current.position.ReadValue() : Vector2.zero;
        public event Action<Vector2> SkillTargeted;
        public event Action SkillCanceled;

        private InputSystem_Actions _actions;
        private Camera _camera;

        public void Start()
        {
            _actions ??= new InputSystem_Actions();

            _actions.SkillTargeting.ConfirmTarget.performed += OnConfirmTarget;
            _actions.SkillTargeting.CancelTarget.performed += OnCancelTarget;
        }

        public void Dispose()
        {
            _actions.SkillTargeting.ConfirmTarget.performed -= OnConfirmTarget;
            _actions.SkillTargeting.CancelTarget.performed -= OnCancelTarget;

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

            _actions.SkillTargeting.Disable();
        }

        private void OnConfirmTarget(InputAction.CallbackContext context)
        {
            SkillTargeted?.Invoke(_camera.ScreenToWorldPoint(PointerPosition));
            DisableSkillMap();
        }

        private void OnCancelTarget(InputAction.CallbackContext context)
        {
            SkillCanceled?.Invoke();
            DisableSkillMap();
        }
    }
}