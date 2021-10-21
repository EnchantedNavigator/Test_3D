using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public  class SceneController : MonoBehaviour
{
    public event System.EventHandler<AsyncOperation> OnLoadedAsyncScene;

    //[SerializeField] GameObject loadingScreen;
    public static SceneController Instance { get; private set; }
    private int idOfSceneToLoad;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
   
        IEnumerator LoadAsync()
    {
        //loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(idOfSceneToLoad);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            OnLoadedAsyncScene?.Invoke(this, asyncLoad);
            yield return null;
        }
        OnLoadedAsyncScene?.Invoke(this, asyncLoad);

    }

    public void GoToMain()
    {
        idOfSceneToLoad = 0;
        StartCoroutine(LoadAsync());

    }

    public void GoToMission(int id)
    {
        idOfSceneToLoad = 1;
        GameManager.Instance.SetCurrentSelectIndexMission(id);
        StartCoroutine(LoadAsync());
    }

}
