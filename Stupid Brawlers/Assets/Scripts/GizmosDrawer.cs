using UnityEngine;

public class EnemyGizmosDrawer : MonoBehaviour
{
    [SerializeField] private Vector3 _sizeEnemyPrefab;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_sizeEnemyPrefab.x, _sizeEnemyPrefab.y, 0));
    }
}