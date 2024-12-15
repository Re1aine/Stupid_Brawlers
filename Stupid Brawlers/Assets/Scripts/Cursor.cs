using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Sprite _scopeSprite;
    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private void Start() => 
        Instantiate(_scopeSprite, transform.position, Quaternion.identity);

    private void Update()
    {
        transform.position = new Vector3(
            _camera.ScreenToWorldPoint(Input.mousePosition).x,
            _camera.ScreenToWorldPoint(Input.mousePosition).y, 
            0);
    }
}

