using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class Completed_Screen : Base_Screen
{

    [SerializeField] GameObject pannelForAnimation;
    Tween fromBottom;
    Tween goUp;
    
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private List<Image> stars;
    [SerializeField] private Sprite lockedStar;
    [SerializeField] private Sprite unlockedStar;
    [SerializeField] TMP_Text completeTimeField;
    [SerializeField] TMP_Text enemiesKilledField;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] float scoreAnimationDuration;
    [SerializeField] float scoreAnimationStep;

    public void Init(CompleteScore score)
    {
        int amountOfStars = score.Stars;
        amountOfStars = Mathf.Clamp(amountOfStars, 0, stars.Count);
        StartCoroutine(StarsAnimationCoroutine(amountOfStars));
        StartCoroutine( AnimateCompleteTimeText(score.CompleteTime,scoreAnimationDuration,(float)scoreAnimationStep, completeTimeField));
        string EnemiesKilledText = enemiesKilledField.text + score.EnemiesKilled.ToString();
        StartCoroutine(AnimateEnemiesKilledText(EnemiesKilledText, scoreAnimationDuration, enemiesKilledField));
    }
    private void StarsAnimate(int id,int amountOfStars)
    {

        stars[id].sprite = id < amountOfStars ? unlockedStar : lockedStar;

    }
    private IEnumerator StarsAnimationCoroutine(int amountOfStars )
    {

        for (int i = 0; i < amountOfStars; i++)
        {
            Tween stars1 = stars[i].transform.DOScale((float)1.4, scoreAnimationDuration / 2).SetDelay(scoreAnimationDuration / 4);
            Tween stars2 = stars[i].transform.DOScale((float)1, scoreAnimationDuration / 2).SetDelay((scoreAnimationDuration / 4) + scoreAnimationDuration/2);

            yield return stars1.WaitForCompletion();
            StarsAnimate(i, amountOfStars);
        }

    }
    IEnumerator AnimateCompleteTimeText(float endValue,float duration,float stepSize,TMP_Text textToAnimate)
    {
        float n = duration / stepSize;
        float valuePerIteration = endValue / n;
        float iterationValue = 0;
        for ( int i = 0; i<n; i++)
        {
            iterationValue += valuePerIteration;
            textToAnimate.text ="Complete Time: " + string.Format("{0:0.00}",iterationValue);
            yield return new WaitForSeconds(stepSize);
        }
        textToAnimate.text = "Complete Time: " + endValue.ToString();
    }
    IEnumerator AnimateEnemiesKilledText(string endValue, float duration, TMP_Text textToAnimate)
    {
        float stepSize = duration/endValue.Length;
        float n = duration / stepSize;
        string iterationValue = "";
        for (int i = 0; i < n; i++)
        {
            iterationValue += endValue[i];
            textToAnimate.text = iterationValue;
            yield return new WaitForSeconds(stepSize);
        }
        textToAnimate.text = endValue;
    }

    public override void Hide()
    {
        goUp = transform.DOLocalMoveY(1000, (float)0.5).SetEase(Ease.InSine);
        goUp.OnComplete(Disable);
    }

    private void OnEnable()
    {
        transform.Translate(0, -1000, 0);
        ToCenter();
    }
    public void ToCenter()
    {
        fromBottom = transform.DOLocalMoveY(0, (float)0.8).SetEase(Ease.InSine);
        fromBottom = pannelForAnimation.transform.DOPunchPosition(new Vector3(0, -100, 0), (float)0.5, 2, 1).SetDelay((float)0.8);


    }
    private void Disable()
    {
        gameObject.SetActive(false);
        SelectMainScreen();
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public void SelectMissionScreen()
    {

        MyManager.OpenPanel(ScreenType.Mission);
        MyManager.ClosePanel(this);
    }
    private void SelectMainScreen()
    {
        loadingScreen.SetActive(true);
        SceneController.Instance.GoToMain();
    }

}
