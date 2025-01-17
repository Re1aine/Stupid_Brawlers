using UnityEngine;

public class PlayerGizmosDrawer : MonoBehaviour
{
    [SerializeField] private Vector3 _sizePlayerPrefab;
    [SerializeField] private float _offsetY;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, _offsetY, 0), new Vector3(_sizePlayerPrefab.x, _sizePlayerPrefab.y, 0));
    }
}