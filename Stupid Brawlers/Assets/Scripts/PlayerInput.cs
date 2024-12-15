using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnPlayerWantsToShoot;
    public event Action<Vector3> OnUpdateShootDirection; 
    
    private Camera _camera;
    
    private Vector3 _shootPointPosition;
    
    private void Awake() => _camera = Camera.main;

    public void SetShootPosition(Vector3 position) =>
        _shootPointPosition = position;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsPlaceShootable())
        {
            var shootDirection = CalculateShootDirection();
            OnUpdateShootDirection?.Invoke(shootDirection);
            OnPlayerWantsToShoot?.Invoke();
        }
    }
   
    private Vector3 CalculateShootDirection()
    {
        Vector3 mousePosition = new Vector3(
            _camera.ScreenToWorldPoint(Input.mousePosition).x,
            _camera.ScreenToWorldPoint(Input.mousePosition).y,
            0 );
        
        return (mousePosition - _shootPointPosition).normalized;
    }

    public void Lock() => gameObject.SetActive(false);

    public void UnLock() => gameObject.SetActive(true);

    private bool IsPlaceShootable()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        if (hit.collider != null && hit.collider.TryGetComponent(out UnTouchZoneMark zone))
        {
            return false;
        }
        
        return true;
    }
}