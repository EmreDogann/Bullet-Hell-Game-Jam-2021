using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public static bool isLoading = false;
    public static bool isUnloading = false;
    
    public static void LoadAdditive(int sceneIndex, UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode> callback = null) {
        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        
        if (callback != null) SceneManager.sceneLoaded += callback;
    }
    
    public static void LoadAdditive(string sceneName, UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode> callback = null) {
        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        if (callback != null) SceneManager.sceneLoaded += callback;
    }
    
    public static void LoadSingle(string sceneName) {
        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    
    public static void Unload(int sceneIndex, UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene> callback = null) {
        isUnloading = true;
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneIndex);
        
        if (callback != null) SceneManager.sceneUnloaded += callback;
        SceneManager.sceneUnloaded += sceneUnloaded;
    }

    static void sceneUnloaded(Scene scene) {
        isUnloading = false;
        SceneManager.sceneUnloaded -= sceneUnloaded;
    }
}