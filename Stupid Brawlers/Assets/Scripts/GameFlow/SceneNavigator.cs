using System;
using UnityEngine.SceneManagement;

public static class SceneNavigator
{
    private const int BootstrapSceneBuildIndex = 0;
    private const int MenuSceneBuildIndex = 1;

    private const string BootstrapSceneName = "BOOTSTRAP";
    private const string MenuSceneName = "MENU";

    private static readonly int OverallSceneCount = SceneManager.sceneCountInBuildSettings;
    private static readonly int LevelSceneOffset = Enum.GetValues(typeof(GameScene)).Length;

    public static string GetName(GameScene gameScene) => 
        (int)gameScene == BootstrapSceneBuildIndex ? BootstrapSceneName : MenuSceneName;

    public static GameScene GetCurrentGameScene() => 
        SceneManager.GetActiveScene().buildIndex == BootstrapSceneBuildIndex ? GameScene.Bootstrap : GameScene.Menu;

    public static string GetNextLvlScene()
    {
        int nextLvlSceneIndex = GetLvlSceneIndex() + 1;

        if (IsLastLvlScene(nextLvlSceneIndex))
            return GetLastLvlName();
        
        if (IsLvlExist(nextLvlSceneIndex) != true)
            return string.Empty;

        return GetSceneNameByIndex(nextLvlSceneIndex);
    }

    private static string GetNextSceneName()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (!IsSceneExist(nextSceneIndex))  
            return string.Empty;
            
        return SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;
    }

    public static string GetCurrentLvlName() => 
        SceneManager.GetActiveScene().name;

    private static bool IsLastLvlScene(int index)
    {
        return index == GetLvlCount() + 1;
    }

    private static int GetLvlSceneIndex() => 
        SceneManager.GetActiveScene().buildIndex;

    public static string GetLvlSceneNameByIndex(int index)
    {
        if (IsLvlExist(index) != true)
            return String.Empty;

        var lvlIndex = index + 1;
        return GetSceneNameByIndex(lvlIndex);
    }
    
    private static string GetSceneNameByIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        
        return sceneName;
    }

    public static string GetFirstLvlName()
    {
        var firstLvlIndex = GetFirstLvlSceneIndex();
        return GetSceneNameByIndex(firstLvlIndex);
    }

    private static string GetLastLvlName()
    {
        var lastLvlSceneIndex = GetLvlCount() + 1;
        return GetSceneNameByIndex(lastLvlSceneIndex);
    }

    private static bool IsSceneExist(int buildIndex) => 
        buildIndex >= 0 && buildIndex <= OverallSceneCount;

    private static bool IsLvlExist(int index) => 
        index > 0 && index <= GetLvlCount();

    private static int GetLvlCount() => 
        OverallSceneCount - GetReserveSceneCount();

    private static int GetReserveSceneCount() => 
        Enum.GetValues(typeof(GameScene)).Length;

    private static int GetFirstLvlSceneIndex() => 
        Enum.GetValues(typeof(GameScene)).Length;
}

