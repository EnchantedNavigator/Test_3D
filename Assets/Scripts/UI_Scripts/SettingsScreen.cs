using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsScreen : Base_Screen
{
   
    // [SerializeField] SceneController sceneController;
    [SerializeField] GameObject pannelForAnimation;
    Tween fromBottom;
    Tween goUp;
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
    public override void Hide()
    {
        goUp = transform.DOLocalMoveY(-1000, (float)0.5).SetEase(Ease.InSine);
        goUp.OnComplete(Disable);

    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
    public void SelectMainScreen()
    {

        MyManager.ClosePanel(this);
        MyManager.OpenPanel(ScreenType.Main);
    }

    public override void Show()
    {
        gameObject.SetActive(true);

    }
}
