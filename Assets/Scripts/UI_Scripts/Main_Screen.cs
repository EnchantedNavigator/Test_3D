using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Screen : Base_Screen
{

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public  void SelectMissionScreen()
    {

        MyManager.OpenPanel(ScreenType.Mission);
        MyManager.ClosePanel(this);
    }
}
