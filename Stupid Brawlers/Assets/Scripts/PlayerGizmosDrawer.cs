using UnityEngine;

public class PlayerGizmosDrawer : MonoBehaviour
{
    [SerializeField] private Vector3 _sizePlayerPrefab;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_sizePlayerPrefab.x, _sizePlayerPrefab.y, 0));
    }
}