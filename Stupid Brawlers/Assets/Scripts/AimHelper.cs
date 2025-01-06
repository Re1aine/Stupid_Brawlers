using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimHelper : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float SpeedAnim;

    private Camera _camera;

    private Color _showColor;
    private Color _hideColor;
    
    private void Awake()
    {
        _camera = Camera.main;
        
        _showColor = new Color(_line.material.color.r, _line.material.color.g, _line.material.color.b, 1);
        _hideColor = new Color(_line.material.color.r, _line.material.color.g, _line.material.color.b, 0);

        _line.material.color = _hideColor;
    }
    
    
    public void SetStartPoint(Vector3 point)
    {
        _line.SetPosition(0, new Vector3(point.x, point.y, 0));
    }

    public Vector3 GetStartPoint() => _line.GetPosition(0);

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
    
    
    [ContextMenu("Hide")]
    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideLine());
    }
    
    [ContextMenu("Show")]
    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowLine());
    }

    private IEnumerator HideLine()
    {
        float elapsedTime = 0f;
        
        while (_line.material.color.a >= 0)
        {
            elapsedTime += Time.deltaTime;
            
            _line.material.color = Color.Lerp(
                a: _line.material.color,
                b: _hideColor,
                t: elapsedTime / SpeedAnim);
            
            yield return null;
        }
    }

    private IEnumerator ShowLine()
    {
        float elapsedTime = 0f;
        
        while (_line.material.color.a <= 0.5f)
        {
            elapsedTime += Time.deltaTime;
            
            _line.material.color = Color.Lerp(
                a: _line.material.color,
                b: _showColor,
                t: (elapsedTime / SpeedAnim));
            
            yield return null;
        }
    }
}