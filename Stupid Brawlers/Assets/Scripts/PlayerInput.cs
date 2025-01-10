using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<AimMode> OnEnterAimMode;
    public event Action OnPlayerWantsToShoot;
    public event Action<Vector3> OnUpdateShootDirection; 
    
    private Camera _camera;
    
    private Vector3 _shootPointPosition;
    private bool _isAimUnActiveTriggered;
    
    private void Awake() => _camera = Camera.main;

    public void SetShootPosition(Vector3 position) =>
        _shootPointPosition = position;
    
    private void Update() => HandleMouseInput();

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
            HandleMouseDown();

        if (Input.GetMouseButton(0))
            HandleMouseHold();
        
        if(Input.GetMouseButtonUp(0))
            HandleMouseUp();
    }


    private void HandleMouseDown()
    {
        _isAimUnActiveTriggered = false;
        if (IsPlaceShootable())
            OnEnterAimMode?.Invoke(AimMode.FullAimActive);
        else
            OnEnterAimMode?.Invoke(AimMode.FullAimUnActive);
    }

    private void HandleMouseHold()
    {
        if (!IsPlaceShootable() && !_isAimUnActiveTriggered)
        {
            OnEnterAimMode?.Invoke(AimMode.FullAimUnActive);
            _isAimUnActiveTriggered = true;
        }

        if (IsPlaceShootable() && _isAimUnActiveTriggered)
        {
            OnEnterAimMode?.Invoke(AimMode.FullAimActive);
            _isAimUnActiveTriggered = false;
        }
    }

    private void HandleMouseUp()
    {
        if (IsPlaceShootable())
        {
            var shootDirection = GetShootDirection();
            OnUpdateShootDirection?.Invoke(shootDirection);
            OnPlayerWantsToShoot?.Invoke();

            OnEnterAimMode?.Invoke(AimMode.OnlyScopeActive);
        }

        _isAimUnActiveTriggered = true;
    }

    public void Run() => gameObject.SetActive(true);

    public void Lock() => gameObject.SetActive(false);

    public Vector3 GetShootDirection()
    {
        Vector3 mousePosition = GetMousePosition();
        return (mousePosition - _shootPointPosition).normalized;
    }

    private bool IsPlaceShootable()
    {
        var mousePos = GetMousePosition();
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent(out UnTouchZoneMark zone))
            return false;
        
        return true;
    }

    public Vector3 GetMousePosition()
    {
        return new Vector3(
            _camera.ScreenToWorldPoint(Input.mousePosition).x,
            _camera.ScreenToWorldPoint(Input.mousePosition).y,
            0 );
    }
}