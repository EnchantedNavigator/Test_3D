using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Main_Screen : Base_Screen
{
    [SerializeField] GameObject pannelForAnimation;
    Tween fromBottom;
    Tween goUp;
    public override void Hide()
    {
        goUp = transform.DOLocalMoveY(1000, (float)0.5).SetEase(Ease.InSine);
        goUp.OnComplete(Disable);
    }
    public override void Show()
    {
        gameObject.SetActive(true);

    }
    private void OnEnable()
    {
        
        ToCenter();
    }
    private void Start()
    {
        transform.Translate(0, -1000, 0);
    }
    public void ToCenter()
    {
        fromBottom = transform.DOLocalMoveY(0, (float)0.8).SetEase(Ease.InSine);
        fromBottom = pannelForAnimation.transform.DOPunchPosition(new Vector3(0, -100, 0), (float)0.5, 2, 1).SetDelay((float)0.8);


    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
    public  void SelectMissionScreen()
    {

        MyManager.OpenPanel(ScreenType.Mission);
        MyManager.ClosePanel(this);
    }
}
