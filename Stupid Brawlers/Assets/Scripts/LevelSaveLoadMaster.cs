using UnityEditor;
using UnityEngine;

public class LevelSaveLoadMaster
{
    public void SetValue(string key, int value) => 
        PlayerPrefs.SetInt(key, value);

    public int GetValue(string key, int defaultValue) => 
        PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;

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