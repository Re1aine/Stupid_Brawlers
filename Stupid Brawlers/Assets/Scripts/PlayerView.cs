using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private const string TargetPoint = "aimt-target-IK";
    public Player Player => _player;
    public GunView GunView => _gunView;
    public PlayerInput Input => _input;

    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private GameObject _shootPoint;
    private IkConstraint _targetConstraint;
    private Camera _camera;
    private Bone _targetBone;
    
    
    [SerializeField] private PlayerInput _input;
    [SerializeField] private GunView _gunView;
    [SerializeField] private AimHelper _aimHelpLine;
    [SerializeField] private Cursor _scope;

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
        TurnOffAim();
        _camera = Camera.main;
        _targetConstraint = _skeletonAnimation.Skeleton.IkConstraints.Find(c => c.ToString() == TargetPoint);
        _targetBone = _targetConstraint.Target;
        
    }

    private void Update()
    {
        Vector3 mousePosition = new Vector3(
            _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).x,
            _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).y,
            0 );
        
        Vector3 direction = (mousePosition - _shootPoint.transform.position).normalized;
        
//        Vector2 originalPosition = new Vector2(_targetBone.X, _targetBone.Y);
//
//// Переводим мировую позицию мыши в локальные координаты
//        var skeletonSpacePoint = _skeletonAnimation.transform.InverseTransformPoint(mousePosition);
//
//// Сохраняем относительное смещение
//        skeletonSpacePoint.x += originalPosition.x;
//        skeletonSpacePoint.y += originalPosition.y;
//
//// Устанавливаем позицию кости
//        _targetBone.SetLocalPosition(skeletonSpacePoint);
        
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