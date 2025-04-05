using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Entity _playerEntity;
    
    // ============================= INIT =============================
    void Awake()
    {
        _playerEntity = player.GetComponent<Entity>();
    }
    
    // =========================== TRIGGERS ===========================
    [UsedImplicitly]
    void OnMove(InputValue value)
    {
        _playerEntity.moveDirectionVector = value.Get<Vector2>();
    }

    // roll/dash
    // [UsedImplicitly]
    // void OnSprint(InputValue value)
    // {
    //     _playerEntity.isBoosting = value.isPressed;
    // }

    [UsedImplicitly]
    void OnJump(InputValue value)
    {
        _playerEntity.isJumping = value.isPressed;
    }
}
