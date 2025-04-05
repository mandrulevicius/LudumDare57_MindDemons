using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Camera _mainCamera;
    
    [SerializeField] GameObject player;
    Entity _playerEntity;
    
    PlayerInput _playerInput;
    
    // ============================= INIT =============================
    void Awake()
    {
        _playerEntity = player.GetComponent<Entity>();
        _playerInput = GetComponent<PlayerInput>();
        _mainCamera = Camera.main;
    }
    
    // =========================== TRIGGERS ===========================
    [UsedImplicitly]
    void OnMove(InputValue value)
    {
        _playerEntity.moveDirectionVector = value.Get<Vector2>();
    }

    [UsedImplicitly]
    void OnSprint(InputValue value)
    {
        _playerEntity.isSprinting = value.isPressed;
    }

    [UsedImplicitly]
    void OnJump(InputValue value)
    {
        _playerEntity.isDashing = value.isPressed;
    }

    [UsedImplicitly]
    void OnLook(InputValue value)
    {
        if (value.Get<Vector2>() == Vector2.zero) return;
        if(!player) return;
        if (_playerInput.currentControlScheme == "Gamepad") _playerEntity.lookDirectionVector = value.Get<Vector2>();
        else if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            // this is whatever
            // Vector2 targetPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // Vector2 direction = targetPos - (Vector2)player.transform.position;

            _playerEntity.lookDirectionVector = (_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue())
                                                 - player.transform.position).normalized;
        }
        else if (_playerInput.currentControlScheme == "Touch")
        {
            // lookDirectionVector based on touch position, like mouse
            // (or control with touch wheel, like gamepad)
        }
    }
}
