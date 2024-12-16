using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    public void SetValue(int value) => _text.text = value.ToString();
    
    private void Destroy() => Destroy(gameObject);
}