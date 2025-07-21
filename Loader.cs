using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        GameScene2,
        Garden,
        Credits,
        LevelSelection,
        CutScene,
        LoadingScreen
    }

    private static Scene targetScene;

    public static void Load(Scene scene)
    {
        targetScene = scene;
        SceneManager.LoadScene(Scene.LoadingScreen.ToString());
    }

    public static void LoaderCallback()
    {
       
        SceneManager.LoadScene(targetScene.ToString());
    }
}
