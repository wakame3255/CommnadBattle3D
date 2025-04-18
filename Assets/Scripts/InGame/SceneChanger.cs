using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScenesNames
{
    StartMenuScene,
    ClearScene,
    GameOverScene,
    MainScene,
    none
}

public class SceneChanger : MonoBehaviour
{
    // シーン名を列挙型で管理


    [SerializeField]
    private ScenesNames targetScene;

    public void ChangeScene()
    {
        string sceneName = targetScene.ToString();

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not in Build Settings. Please add it.");
        }
    }

    public void End()
    {
        Application.Quit();
    }

    public void ChangeScene(ScenesNames scene)
    {
        string sceneName = scene.ToString();

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not in Build Settings. Please add it.");
        }
    }
}
