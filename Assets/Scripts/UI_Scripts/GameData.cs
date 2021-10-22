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
        Save(missionDatas,MissionSaveKey);
    }
    public static void Save<T>(T data,string key)
    {
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(key, json);

    }
    public static T Load<T>( string key,System.Action onDataNull = null)
    {
        T result = default;
        if(PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            result = JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            onDataNull?.Invoke();
           
        }
        return result;
    }


    public static List<MissionData> GetMissionData()
    {
        List<MissionData> result;
        result = Load<List<MissionData>>(MissionSaveKey);
        if (result.Count < 1 )
        {
            result = new List<MissionData>();
            result.Add(new MissionData() { Id = 0, IsLocked = false, Stars = 0 });
            Save(result,MissionSaveKey);
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
   

