using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Missions_Screen : Base_Screen
{
    private List<MissionData> missionDatas;
    [SerializeField]
    private List<MissionButton> missions;
    [SerializeField]
    private Button okButton;
    [SerializeField] GameObject loadingScreen;
    private int okButtonMissionId;
    private bool isOkButtonHasMissionId;
    private MissionButton _chosenMission;
    private MissionButton _previousChosenMission;
   // [SerializeField] SceneController sceneController;
    [SerializeField] GameObject pannelForAnimation;
    Tween fromBottom;
    Tween goUp;
    private void Awake()
    {
        missions.ForEach(x => x.myManager = this);
    }
    private void Start()
    {
        okButton.interactable = false;
        isOkButtonHasMissionId = false;
        loadingScreen.SetActive(false);
        missionDatas = GameData.GetMissionData();
        for (int i = 0; i < missions.Count; i++)
        {
            MissionData foundData = missionDatas.Find(x => x.Id == missions[i].Id);
            missions[i].Init(foundData);
        }
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
    public void GoToMission()
    {
        loadingScreen.SetActive(true);
        SceneController.Instance.GoToMission(okButtonMissionId);

    }

    public override void Show()
    {
        gameObject.SetActive(true);

    }
    public void Select(MissionButton mission)
    {
        if (mission.IsButtonLocked == false)
        {
            _previousChosenMission = _chosenMission;
            _chosenMission = mission;
            if (_previousChosenMission != null)
            {
                _previousChosenMission.UnSelected();
            }
            _chosenMission.Selected();
            okButton.interactable = true;
            okButtonMissionId = _chosenMission.Id;
            isOkButtonHasMissionId = true;

        }
    }
    public void OnOkButtonClick()
    {
        if (isOkButtonHasMissionId == true)
        {
            Hide();
            goUp.OnComplete(GoToMission);
            GameManager.Instance.SetCurrentSelectIndexMission(okButtonMissionId);
        }
    }
}