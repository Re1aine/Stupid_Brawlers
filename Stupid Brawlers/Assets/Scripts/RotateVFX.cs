using UnityEngine;

public class RotateVFX : MonoBehaviour
{
    [SerializeField] private int _speed; 
    private void Update() => transform.Rotate(0, 0, -(_speed * Time.deltaTime));
}
