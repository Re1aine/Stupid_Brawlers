using UnityEditor;
using UnityEngine;

public class LevelSaveLoadMaster
{
    public void SetValue<T>(string key, T value) => 
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));

    public T GetValue<T>(string key, T defaultValue) => 
        PlayerPrefs.HasKey(key) ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key)) : defaultValue;

    public void Save()
    {
        PlayerPrefs.Save();
    }
    
    [MenuItem("Tools/PlayerPrefs/Clear")]
    private static void Clear()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared!");
    }
}

