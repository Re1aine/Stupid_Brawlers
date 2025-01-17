using UnityEngine;

public static class AssetProvider
{
    public static T Instantiate<T>(string prefabName) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public static T InstantiateAt<T>(string prefabName, Vector3 position) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public static T Instantiate<T>(string prefabName, Transform parent) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    }
}