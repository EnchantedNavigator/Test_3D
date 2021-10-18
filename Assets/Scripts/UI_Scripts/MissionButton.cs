using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionButton : MonoBehaviour
{

    public Missions_Screen myManager;
    [Header("UI")]
    [SerializeField] private Image missionImage;
    [SerializeField] private Sprite lockedMission;
    [SerializeField] private Sprite unlockedMission;
    [SerializeField] private Sprite lockedStar;
    [SerializeField] private Sprite unlockedStar;
    [Header("Mission")]
    [SerializeField] private List<Image> stars;
    [SerializeField] private Image _lock;
    [SerializeField] private MissionData myMission;
    [SerializeField] private int buttonId;
    private bool isButtonLocked;
    public int Id { get => buttonId; }
    public bool IsButtonLocked { get => isButtonLocked; }

    public void Init(MissionData data)
    {
        myMission = data;
        if (data == null)
        {
            SetState(true, 0);
        } else
        if (myMission.IsLocked == true)
        {
            SetState( true, 0);
        } else
        if (data != null && myMission.IsLocked == false)
        {
            SetState( false, myMission.Stars);
        }

    }
    private void SetState(bool isLocked, int amountOfStars)
    {
        if (isLocked == true)
        {
            missionImage.sprite = lockedMission;
            _lock.color = Color.white;
            StateStars(0);
            isButtonLocked = true;

        }else
        {
            missionImage.sprite = unlockedMission;
            _lock.color = Color.clear;
            isButtonLocked = false;
            if (amountOfStars == 0) { stars.ForEach(x => x.sprite = lockedStar); }
            else StateStars(amountOfStars);
        }
    }
    private void StateStars(int amountOfStars)
    {
        amountOfStars = Mathf.Clamp(amountOfStars,0,stars.Count);
        for (int i=0;i<stars.Count;i++)
        {
            stars[i].sprite = i < amountOfStars ? unlockedStar : lockedStar;
        }
    }
    public void Selected()
    {
        missionImage.color = Color.green;
    }
    public void UnSelected()
    {
        missionImage.color = Color.white;

    }
    public void ButtonClicked()
    {
        myManager.Select(this);
    }
}
