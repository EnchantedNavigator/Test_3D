using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSettings
{
    
    [SerializeField] private int id;
    [SerializeField][Range(1,10)] private int countSpawnEnemies;
    [SerializeField] private float zeroStarCompleteTime;
    [SerializeField] private float oneStarCompleteTime;
    [SerializeField] private float twoStarCompleteTime;
    [SerializeField] private float threeStarCompleteTime;
    public int CountSpawnEnemies { get => countSpawnEnemies; }
    public int Id { get => id;}
    public float OneStarCompleteTime { get => oneStarCompleteTime; }
    public float ZeroStarCompleteTime { get => zeroStarCompleteTime;}
    public float TwoStarCompleteTime { get => twoStarCompleteTime;}
    public float ThreeStarCompleteTime { get => threeStarCompleteTime;}


}
