using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : DescriptionBaseSO, GameInput.IGameplayActions //, GameInput.IMenusActions, GameInput.ICheatsActions
{
    // Assign delegate{} to events to initialise them with an empty delegate
    // so we can skip the null check when we use them

    // Gameplay
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> CameraMoveEvent = delegate { };
    public event UnityAction AimEvent = delegate { };
    public event UnityAction AimCanceledEvent = delegate { };
    public event UnityAction AttackEvent = delegate { };
    public event UnityAction AttackCanceledEvent = delegate { };
    public event UnityAction SpawnBaitEvent = delegate { };

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            
            _gameInput.Gameplay.SetCallbacks(this);
            //_gameInput.Menus.SetCallBacks(this);
            //_gameInput.Cheats.SetCallbacks(this);
        }

#if UNITY_EDITOR
        //_gameInput.Cheats.Enable();
#endif
    }
    
    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                AttackEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                AttackCanceledEvent.Invoke();
                break;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        CameraMoveEvent.Invoke(context.ReadValue<Vector2>());
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            AimEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            AimCanceledEvent.Invoke();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            JumpCanceledEvent.Invoke();
    }
    public void OnSpawnBait(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
        SpawnBaitEvent.Invoke();
        }
    }
    public void EnableGameplayInput()
    {
        //_gameInput.Menus.Disable();
        _gameInput.Gameplay.Enable();
    }
    private void DisableAllInput()
    {
        _gameInput.Gameplay.Disable();
        //_gameInput.Menus.Disable();
    }
    public string GetControlScheme()
    {
        return _gameInput.controlSchemes.ToString();
    }
}
