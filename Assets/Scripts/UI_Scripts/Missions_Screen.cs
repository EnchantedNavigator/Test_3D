using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions_Screen : Base_Screen
{
    private List<MissionData> missionDatas;
    [SerializeField]
    private List<MissionButton> missions;
    [SerializeField]
    private Button okButton;
    private int okButtonMissionId;
    private bool isOkButtonHasMissionId;
    private MissionButton _chosenMission;
    private MissionButton _previousChosenMission;
    private void Awake()
    {
        missions.ForEach(x => x.myManager = this);
    }
    private void Start()
    {
        okButton.interactable = false;
        isOkButtonHasMissionId = false;
        missionDatas = GameData.GetMissionData();
        for (int i = 0; i < missions.Count; i++)
        {
            MissionData foundData = missionDatas.Find(x => x.Id == missions[i].Id);
            missions[i].Init(foundData);
        }
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SelectMainScreen()
    {
        MyManager.OpenPanel(ScreenType.Main);
        MyManager.ClosePanel(this);
    }
    public void GoToMission(int id)
    {
        SceneController.GoToMission(id);

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
            GoToMission(okButtonMissionId);
            GameManager.Instance.SetCurrentSelectIndexMission(okButtonMissionId);
        }
    }
}