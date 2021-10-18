using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Completed_Screen : Base_Screen
{
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private List<Image> stars;
    [SerializeField] private Sprite lockedStar;
    [SerializeField] private Sprite unlockedStar;
    [SerializeField] TMP_Text completeTimeField;
    [SerializeField] TMP_Text enemiesKilledField;
    public void Init(CompleteScore score)
    {
        int amountOfStars = score.Stars;
        amountOfStars = Mathf.Clamp(amountOfStars, 0, stars.Count);
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].sprite = i < amountOfStars ? unlockedStar : lockedStar;
        }
        completeTimeField.text = " Complete Time: " + score.CompleteTime.ToString();
        enemiesKilledField.text = " Enemies Killed: " + score.EnemiesKilled.ToString();

    }

    public override void Hide()
    {
        gameObject.SetActive(false);
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
    public void SelectMainScreen()
    {
        SceneController.GoToMain();
    }

}
