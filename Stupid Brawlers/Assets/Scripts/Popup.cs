using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    
    private Animator _animator;

    public void SetValue(int value) => _text.text = value.ToString();
}