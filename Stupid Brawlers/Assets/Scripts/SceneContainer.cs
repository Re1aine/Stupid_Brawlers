using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneContainer : MonoBehaviour
{
    public PlayerSpawnPoint PlayerSpawnPoint => _playerSpawnPoint;
    public List<EnemySpawnPoint> EnemySpawnPoints => _enemySpawnPoints;
    
    private PlayerSpawnPoint _playerSpawnPoint;
    private List<EnemySpawnPoint> _enemySpawnPoints;

    public void Start()
    {
        _playerSpawnPoint = GetComponentsInChildren<PlayerSpawnPoint>().FirstOrDefault();
        _enemySpawnPoints = GetComponentsInChildren<EnemySpawnPoint>().ToList();        
    }
}