using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Camera _mainCamera;
    [SerializeField] GameObject target;
    
    // ============================= INIT =============================
    void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    [UsedImplicitly]
    void OnScrollWheel(InputValue value)
    {
        Vector2 scrollDelta = value.Get<Vector2>();
        if (scrollDelta.y == 0) return;
        if (scrollDelta.y > 0) _mainCamera.orthographicSize /= Mathf.Abs(scrollDelta.y) * 1.1f;
        else _mainCamera.orthographicSize *= Mathf.Abs(scrollDelta.y) * 1.1f;
        // mainCamera.orthographicSize -= scrollDelta.y;
    }
    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        if (target) MoveCameraTo(target.transform.position);
    }
    
    // ========================== FUNCTIONS ===========================
    void MoveCameraTo(Vector3 targetPos)
    {
        // this is not controller code, but can be here for now. maybe wont even need with cinemachine
        targetPos.z = _mainCamera.transform.position.z;
        Vector3 distanceVector = targetPos - _mainCamera.transform.position;
        float smoothFactor = 100f * Time.fixedDeltaTime;
        _mainCamera.transform.position += distanceVector / smoothFactor; // not very smooth, but good enough for now
        //later try something like this:
        // mainCamera.transform.position =
        //  Vector3.SmoothDamp(mainCamera.transform.position, targetPos, ref distanceVector, smoothFactor);
        // or just use cinemachine
    }
}