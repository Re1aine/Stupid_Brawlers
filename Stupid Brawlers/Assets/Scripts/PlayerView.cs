using System;
using Spine.Unity;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Player Player => _player;
    public GunView GunView => _gunView;
    public PlayerInput Input => _input;
    
    [SerializeField] private SkeletonUtilityBone _aimTargetBone;
    [SerializeField] private SkeletonUtility _skeletonUtility;
    
    [SerializeField] private PlayerInput _input;
    [SerializeField] private GunView _gunView;
    [SerializeField] private AimHelper _aimHelpLine;
    [SerializeField] private Cursor _scope;

    [SerializeField] private Transform _view;
    [SerializeField] private float _minDistance;
    [SerializeField] private CapsuleCollider2D _collider;

    private AimHandleMode _aimHandleMode;
    
    private Player _player;

    public void Construct(Player player)
    {
        _player = player;

        _input.SetShootPosition(_gunView.GetShootPoint());
        _input.OnEnterAimMode += SetAimMode;
        _input.OnUpdateShootDirection += _gunView.SetShootDirection;
        _input.OnPlayerWantsToShoot += _gunView.Shoot;
    }

    private void Awake()
    {
        _aimHelpLine.SetStartPoint(_gunView.GetShootPoint());
        TurnOffAim();
    }

    private void Update()
    {
        _scope.transform.position = _input.GetMousePosition();

        if ((_scope.transform.position - _view.transform.position).sqrMagnitude < _minDistance * _minDistance)
        {
            _scope.transform.position = _view.transform.position + _input.GetShootDirection() * _minDistance;
            _aimHelpLine.SetEndPoint(_scope.transform.position);
            
            _aimHelpLine.SetAimHandleMode(AimHandleMode.MeleeRange);
            _scope.SetAimHandleMode(AimHandleMode.MeleeRange);
        }
        else
        {
            _aimHelpLine.SetAimHandleMode(AimHandleMode.LongRange);
            _scope.SetAimHandleMode(AimHandleMode.LongRange);
        }

        _aimTargetBone.transform.position = _scope.transform.position;
        
        FlipPlayer();

        if (_aimHelpLine.gameObject.activeSelf)
        {
            if((_gunView.GetShootPoint() - _aimHelpLine.GetStartPoint()).sqrMagnitude < Mathf.Epsilon) return; 
            _aimHelpLine.SetStartPoint(_gunView.GetShootPoint());
        }
    }
    
    private void FlipPlayer()
    {
        if(_scope.transform.position.x > _skeletonUtility.transform.position.x)
            _skeletonUtility.Skeleton.ScaleX = 1;
        else
            _skeletonUtility.Skeleton.ScaleX = -1;
    }

    private void SetAimMode(AimMode mode)
    {
        TurnOnAim();
        switch (mode)
        {
            case AimMode.FullAimActive: TurnOnFullAimMode();
                break;
            case AimMode.FullAimUnActive: TurnOffFullAimMode();
                break;
            case AimMode.OnlyScopeActive: TurnOnOnlyScopeMode();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
    
    private void TurnOffAim()
    {
        _aimHelpLine.gameObject.SetActive(false);
        _scope.gameObject.SetActive(false);
    }

    private void TurnOnAim()
    {
        _aimHelpLine.gameObject.SetActive(true);
        _scope.gameObject.SetActive(true);
        _scope.StartUpdatePosition();
    }

    private void TurnOnFullAimMode()
    {
        _aimHelpLine.Show();
        _scope.Show();
    }

    private void TurnOffFullAimMode()
    {
        _scope.StopUpdatePosition();
        _aimHelpLine.Hide();
        _scope.Hide();
    }
    
    private void TurnOnOnlyScopeMode()
    {
       _aimHelpLine.Hide();
       _scope.StopUpdatePosition();
    }
}

public enum AimMode
{
    FullAimActive = 0,
    FullAimUnActive = 1,
    OnlyScopeActive = 2,
}

public enum AimHandleMode
{
    LongRange = 0,
    MeleeRange = 1,
}