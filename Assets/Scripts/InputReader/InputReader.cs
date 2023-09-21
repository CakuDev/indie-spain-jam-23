using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    // Player events
    public event Action<Vector2> MoveEvent;
    public event Action InteractEvent;
    public event Action HoldEvent;
    public event Action AttackEvent;
    public event Action ParryEvent;

    // UI events
    public event Action PauseEvent;
    public event Action ResumeEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            HoldEvent?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AttackEvent?.Invoke();
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ParryEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }
}
