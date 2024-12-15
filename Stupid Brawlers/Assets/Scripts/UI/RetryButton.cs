using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RetryLevel);
    }

    private void RetryLevel() => Debug.Log("<b><color=yellow>[LEVEL RELOADED]</color><b>");

}