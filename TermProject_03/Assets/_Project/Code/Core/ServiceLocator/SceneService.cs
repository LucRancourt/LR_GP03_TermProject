using UnityEngine;
using UnityEngine.SceneManagement;

using _Project.Code.Core.ServiceLocator;


public class SceneService : MonoBehaviourService
{
    public string CurrentSceneName { get; private set; }


    public override void Initialize()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += SetActiveScene;
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByBuildIndex(0) != SceneManager.GetActiveScene())
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        LoadSceneAsync(sceneName);
    }

    public async void LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        if (ServiceLocator.TryGet(out LoadingScreen loadingScreen))
        {
            loadingScreen.Activate();

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingScreen.SetLoadValue(progress);

                await System.Threading.Tasks.Task.Yield();
            }

            loadingScreen.Deactivate();
        }
        else
        {
            while (!operation.isDone)
            {
                await System.Threading.Tasks.Task.Yield();
            }
        }
    }

    public void ReloadCurrentScene()
    {
        LoadScene(CurrentSceneName);
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        CurrentSceneName = scene.name;
    }

    public override void Dispose()
    {
    }
}
