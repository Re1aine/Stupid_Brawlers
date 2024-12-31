using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Player Player => _player;
    public GunView GunView => _gunView;
    public PlayerInput Input => _input;
    
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