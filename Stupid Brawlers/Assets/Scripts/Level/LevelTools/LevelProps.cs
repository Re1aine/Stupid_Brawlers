using UnityEngine;

public class LevelProps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Enemy enemy)) enemy.Die();
    }
}