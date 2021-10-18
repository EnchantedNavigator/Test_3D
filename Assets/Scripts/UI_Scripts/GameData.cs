using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class GameData 
{
    private const int CountMissions = 8;
    private const string MissionSaveKey = "MissionSaveKey";
    
    public static void AddMission(MissionData data)
    {
        List<MissionData> missionDatas = GetMissionData();
       
        MissionData foundData = missionDatas.Find(x => x.Id == data.Id);
        if (foundData != null)
        {
            if (foundData.IsEqual(data))
            {
                foundData.Rewrite(data);
            }
        }
        else
        {
            missionDatas.Add(data);
        }
        Save(missionDatas);
    }
    public static void Save(List<MissionData> data)
    {
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(MissionSaveKey, json);

    }

    public static List<MissionData> GetMissionData()
    {
        List<MissionData> result;
        if ( PlayerPrefs.HasKey(MissionSaveKey))
        {
            string json = PlayerPrefs.GetString(MissionSaveKey);
            result = JsonConvert.DeserializeObject<List<MissionData>>(json);
        }
        else
        {
            result = new List<MissionData>();
            result.Add(new MissionData() { Id = 0, IsLocked = false, Stars = 0 });
            Save(result);
        }
        return result;
    }
    public static void SetCompleteLevel(MissionData data)
    {
        AddMission(data);
        if(data != null)
        {
            if (data.Id < CountMissions - 1)
            {
                MissionData dataMissions = GetMissionData().Find(x => x.Id == data.Id + 1);

                if (dataMissions == null)
                {
                    dataMissions = new MissionData() { Id = data.Id + 1, IsLocked = false };
                    AddMission(dataMissions);

                }
            }
        }
    }
}
   

