using UnityEngine;

public class EnemyGizmosDrawer : MonoBehaviour
{
    [SerializeField] private Vector3 _sizeEnemyPrefab;
    [SerializeField] private float _offSetY;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, _offSetY, 0), new Vector3(_sizeEnemyPrefab.x, _sizeEnemyPrefab.y, 0));
    }
}