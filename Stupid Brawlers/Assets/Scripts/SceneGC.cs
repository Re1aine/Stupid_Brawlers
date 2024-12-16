using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class SceneGC : IDisposable
{
    public void AddEntity(MonoBehaviour entity) => _entities.Add(entity);

    private List<MonoBehaviour> _entities = new List<MonoBehaviour>();
    public void Dispose()
    {
        foreach (var e in _entities.Where(e => e != null)) 
            Object.Destroy(e.gameObject);

        Debug.Log("<b><color=purple> [GCSCENE] <color=purple>" +
                  "<color=white> ENTITIES HAS BEEN DESTROYED <color=white>");
    }
}