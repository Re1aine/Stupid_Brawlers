using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private float SpeedAnim;
    
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private AimHandleMode _aimHandleMode;
    
    private Color _showColor;
    private Color _hideColor;

    private bool _isUpdatePos;
    
    private void Awake()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _showColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
        _hideColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);

        _spriteRenderer.color = _hideColor;
        _isUpdatePos = true;
    }

    private void Update()
    {
        if(!_isUpdatePos) return;
        if(_aimHandleMode == AimHandleMode.MeleeRange) return;
        
        transform.position = new Vector3(
            _camera.ScreenToWorldPoint(Input.mousePosition).x,
            _camera.ScreenToWorldPoint(Input.mousePosition).y,
            0);
    }

    public void StopUpdatePosition()
    {
        _isUpdatePos = false;
    }

    public void StartUpdatePosition()
    {
        _isUpdatePos = true;
    }

    public void SetAimHandleMode(AimHandleMode mode) => _aimHandleMode = mode;

    [ContextMenu("Hide")]
    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideCursor());
    }
    
    [ContextMenu("Show")]
    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCursor());
    }

    private IEnumerator HideCursor()
    {
        float elapsedTime = 0f;
        
        while (_spriteRenderer.color.a >= 0)
        {
            elapsedTime += Time.deltaTime;
            
            _spriteRenderer.color = Color.Lerp(
                a: _spriteRenderer.color,
                b: _hideColor,
                t: elapsedTime / SpeedAnim);
            
            yield return null;
        }
    }

    private IEnumerator ShowCursor()
    {
        float elapsedTime = 0f;
        
        while (_spriteRenderer.color.a <= 1)
        {
            elapsedTime += Time.deltaTime;
            
            _spriteRenderer.color = Color.Lerp(
                a: _spriteRenderer.color,
                b: _showColor,
                t: (elapsedTime / SpeedAnim));
            
            yield return null;
        }
    } 
}

