using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    private static int targetScene;

    public static void LoadMainMenu() => LoadWLoading("MainMenu");
    public static void LoadGame() => LoadWLoading("FirstMap");
    public static void LoadTest() => LoadDirectly("TMP");

    public static void AddAscene(int scene) { SceneManager.LoadScene(scene, LoadSceneMode.Additive); }

    public static void AddAscene(string scene) { SceneManager.LoadScene(scene, LoadSceneMode.Additive); }

    public static void LoadWLoading(int scene)
    {
        
        targetScene = scene;
        LoadDirectly("LoadingScene");
        
    }

    public static void LoadWLoading(string scene)
    {
        
        targetScene = GetSceneIndexByName(scene);
        LoadDirectly("LoadingScene");
        
    }

    public static int GetSceneIndexByName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return i;
        }
        return -1;
    }


    public static void LoadDirectly(string scene) { SceneManager.LoadScene(scene); }
    public static void LoadDirectly(int scene) { SceneManager.LoadScene(scene); }


    //private static bool isLoading = false;

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene);


    }






    /*private static int targetScene;

    public static void LoadVRMenu() => LoadDirectly("VRMenu");
    public static void LoadPCMenu() => LoadDirectly("PCMenu");
    public static void LoadGame() => LoadWLoading("GameScene");
    public static void LoadTest() => LoadWLoading("GameCross");

    public static void AddAscene(int scene) { if (IsSceneValid(scene)) SceneManager.LoadScene(scene, LoadSceneMode.Additive); }

    public static void AddAscene(string scene) { if (IsSceneValid(scene)) SceneManager.LoadScene(scene, LoadSceneMode.Additive); }

    public static void LoadWLoading(int scene)
    {
        if (IsSceneValid(scene))
        {
            targetScene = scene;
            LoadDirectly("LoadingScene");
        }
    }

    public static void LoadWLoading(string scene)
    {
        if (IsSceneValid(scene)) {
            targetScene = GetSceneIndexByName(scene);
            LoadDirectly("LoadingScene");
        }
    }

    public static int GetSceneIndexByName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return i;
        }
        return -1;
    }


    public static void LoadDirectly(string scene) { if (IsSceneValid(scene)) SceneManager.LoadScene(scene); }
    public static void LoadDirectly(int scene) { if(IsSceneValid(scene)) SceneManager.LoadScene(scene); }


    private static bool isLoading = false;

    public static void LoaderCallBack()
    {
        if (isLoading) return;
        isLoading = true;

        GameObject runner = new GameObject("LoaderRunner");
        Object.DontDestroyOnLoad(runner);
        runner.AddComponent<LoaderRunner>().StartCoroutine(LoadAsyncAndUnload(runner));
    }

    private static IEnumerator LoadAsyncAndUnload(GameObject runner)
    {
        // validate targetScene
        if (targetScene < 0 || targetScene >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid targetScene index!");
            yield break;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();

        // unload loading scene safely
        AsyncOperation unload = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return unload;

        Object.Destroy(runner);
        isLoading = false;
    }


    public static bool IsSceneValid(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }

        Debug.LogError($"Scene: \"{sceneName}\" doesn't exist in build settings!");
        return false;
    }

    public static bool IsSceneValid(int index)
    {
        // valid if index in range and actually mapped to a build path
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(index);
            if (!string.IsNullOrEmpty(path))
                return true;
        }

        Debug.LogError($"Scene numbered: \"{index}\" doesn't exist in build settings!");
        return false;
    }


    private class LoaderRunner : MonoBehaviour { }*/

}
