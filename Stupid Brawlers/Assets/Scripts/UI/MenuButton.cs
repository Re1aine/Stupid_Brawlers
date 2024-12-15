using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(EntireToMenu);
    }

    private void EntireToMenu() => Debug.Log("<b><color=yellow>[YOU ENTIRE TO MENU]</color><b>");
}