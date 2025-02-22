using UnityEngine;

public class PopupMaster : MonoBehaviour
{
    [SerializeField] private Popup _popUpPrefab;
    public void CreatePopUpAt(Enemy enemy)
    {
        var popUp = Instantiate(_popUpPrefab, enemy.PositionPopUp.position, Quaternion.identity);
        popUp.SetValue(enemy.GetRewardValue());
    }
}