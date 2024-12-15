using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Player Player => _player;
    public GunView GunView => _gunView;
    public PlayerInput Input => _input;
    
    [SerializeField] private PlayerInput _input;
    [SerializeField] private GunView _gunView;
    [SerializeField] private AimHelper _aimHelper;
    
    private Player _player;
    
    public void Construct(Player player)
    {
        _player = player;

        _input.SetShootPosition(_gunView.GetShootPoint());
        _input.OnUpdateShootDirection += _gunView.SetShootDirection;
        _input.OnPlayerWantsToShoot += _gunView.Shoot;
    }

    public void LockInput() => _input.Lock();
}