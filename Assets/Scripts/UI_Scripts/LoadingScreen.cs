using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider progressSlider;
    [SerializeField] Button startButton;
    [SerializeField] TMP_Text loadingText;
    Tween progressAnimation;
    private AsyncOperation currentLoadLevelAsync;
    // Start is called before the first frame update
    private void Start()
    {
        SceneController.Instance.OnLoadedAsyncScene += SceneController_OnLoadedAsyncScene;
        
    }

    private void SceneController_OnLoadedAsyncScene(object sender, AsyncOperation e)
    {

        if (currentLoadLevelAsync != e)
        {
            currentLoadLevelAsync = e;
            currentLoadLevelAsync.allowSceneActivation = false;
            _loadingScreen.transform.Translate(400, 0, 0);

        }
        if (currentLoadLevelAsync.progress <.09f)
        {
            progressAnimation = progressSlider.DOValue(currentLoadLevelAsync.progress, (float)0.3);
        }
        if (currentLoadLevelAsync.progress >=.09f && !currentLoadLevelAsync.allowSceneActivation)
        {
            progressAnimation = progressSlider.DOValue(1, (float)0.3);
            loadingText.text = " Press START to Continue";

            startButton.interactable = true;

        }

    }
    public void OnStartButtonClick()
    {
        if (currentLoadLevelAsync != null)
        {
            SceneController.Instance.OnLoadedAsyncScene -= SceneController_OnLoadedAsyncScene;
            _loadingScreen.SetActive(false);
            currentLoadLevelAsync.allowSceneActivation = true;
        }

    }
}
