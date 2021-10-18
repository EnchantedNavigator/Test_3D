using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData 
{
    public int Id { get; set; }
    public bool IsLocked { get; set; }
    public int Stars { get; set; }
    public void Rewrite (MissionData data)
    {
        Id = data.Id;
        IsLocked = data.IsLocked;
        Stars = data.Stars;
    }
    public bool IsEqual(MissionData data)
    {
        bool result = false;
        if (Stars < data.Stars)
        {
            result = true;
        }
        return result;
    }
}
