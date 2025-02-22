using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _reboundCount;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _reboundSound;
    
    private AudioService _audioService;
    
    private Rigidbody2D _rigidbody2D;
    private TrailRenderer _trailRenderer;

    private Vector3 _direction;
    private int _remainBound;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _remainBound = _reboundCount;
    }

    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }
    
    public void SetMoveDirection(Vector3 direction) => _direction = direction;

    private void FixedUpdate() => MoveToDirection(_direction);

    private void MoveToDirection(Vector3 direction) => 
        _rigidbody2D.linearVelocity = direction * _speed;

    private void ReboundBullet(Vector2 normal)
    {
        _audioService.PlayShortSound(_reboundSound);
        
        _remainBound -= 1;
        
        var reflectDirection = Vector2.Reflect(_direction, normal).normalized;
        RotateToDirection(reflectDirection);
        
        _direction = reflectDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ReboundSurfaceMark surface) & _remainBound > 0)
        {
            var normal = other.GetContact(0).normal;
            ReboundBullet(normal);
        }
        else if (other.gameObject.TryGetComponent(out Enemy enemy))
            enemy.Die();
        else
        {
            _audioService.PlayShortSound(_reboundSound);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy)) enemy.Die();          
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

