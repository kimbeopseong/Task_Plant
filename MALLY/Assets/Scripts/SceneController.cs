using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNames
{
    public static string LoginScene = "LoginScene";
    public static string LoadingScene = "LoadingScene";
    public static string StartScene = "StartScene";
}

public class SceneController : MonoBehaviour
{
    private static SceneController instance = null;
    public static SceneController Instance
    {
        get 
        {
            if(instance == null)
            {
                GameObject moveScene = GameObject.Find("SceneController");
                if(moveScene == null)
                {
                    moveScene = new GameObject("SceneController");
                    
                    SceneController controller = moveScene.AddComponent<SceneController>();
                    return controller;
                }
            }
            return instance;
        }
    }

    void Awake() 
    {
        if(instance != null)
        {
            Debug.LogWarning("Can't have two instances of singletone");
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Single));
    }
    public void LoadSceneAdditive(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }
    IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        while(!operation.isDone)
            yield return null;

        Debug.Log("LoadScene is complete!");
    }

    public void OnActiveSceneChanged(Scene s0, Scene s1)
    {
        Debug.Log("OnActiveSceneChanged is called! scene0 = " + s0.name + ", scene1 = " + s1.name);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded is called! scene = " + scene.name + ", loadSceneMode = " + loadSceneMode.ToString());
    }
    public void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("OnSceneUnLoaded is called! scene = " + scene.name);
    }

}
