using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimHelper : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    
    private LineRenderer _line;
    private Camera _camera;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _line.SetPosition(0, new Vector3(
            _shootPoint.position.x,
            _shootPoint.position.y,
            0));
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        
        Vector2 direction = (mouseWorldPosition - _shootPoint.position).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position, direction, Mathf.Infinity, LayerMask.GetMask("Default"));

        if (hit.collider != null)
            _line.SetPosition(1, hit.point);
        else
        {
            _line.SetPosition(1, 
                new Vector3(
                    _camera.ScreenToWorldPoint(Input.mousePosition).x,
                    _camera.ScreenToWorldPoint(Input.mousePosition).y,
                    0));
        }
    }
}