using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public event Action<Bullet> OnDestroyed;
    
    [SerializeField] private int _reboundCount;
    [SerializeField] private float _speed;

    private LevelDispatcher _levelDispatcher;
    
    private Rigidbody2D _rigidbody2D;
    private TrailRenderer _trailRenderer;

    private Vector3 _direction;
    private int _remainBound;


    public void Construct(LevelDispatcher levelDispatcher)
    {
        _levelDispatcher = levelDispatcher;
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _remainBound = _reboundCount;
    }

    public void SetMoveDirection(Vector3 direction) => _direction = direction;

    private void FixedUpdate() => MoveToDirection(_direction);

    private void MoveToDirection(Vector3 direction) => 
        _rigidbody2D.linearVelocity = direction * _speed;

    private void ReboundBullet(Vector2 normal)
    {
        _remainBound -= 1;
        
        var reflectDirection = Vector2.Reflect(_direction, normal).normalized;
        RotateToDirection(reflectDirection);
        
        _direction = reflectDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall) & _remainBound > 0)
        {
            var normal = other.GetContact(0).normal;
            ReboundBullet(normal);
        }
        else if (other.gameObject.TryGetComponent(out Enemy enemy))
            enemy.Die();
        else
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void RotateToDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void FreezeMove()
    {
        _rigidbody2D.simulated = false;
        _trailRenderer.time = Mathf.Infinity;
    }
}

