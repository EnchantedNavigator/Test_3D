using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelController : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    [SerializeField] private int levelId;
    [SerializeField] private GameObject stars;
    [SerializeField] private Button okButton;
    [SerializeField] private EnemySpawn spawner; 
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private Completed_Screen completed_Screen;
    [SerializeField] private List<LevelSettings> levelSettings;
    private int allEnemies;
    private int aliveEnemies;
    private CompleteScore score = new CompleteScore();
    private MissionData data = new MissionData();
    private float gamecompleteTime;
    private bool isGameFinished;
    private int completedStars;
    private int countOfEnemies;

    private LevelSettings mySettings;
    private List<Enemy> enemies;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        enemies.ForEach(x => x.OnDied -= EnemyDied);
    }
    public void Init(int id)
    {
        mySettings = levelSettings.Find(x => x.Id == id);
        if(mySettings != null)
        {
            countOfEnemies = mySettings.CountSpawnEnemies;
        }
        levelId = id;
    }
    private void EnemyDied(object sender, System.EventArgs e)
    {
        Enemy enemy = sender as Enemy;
        enemy.OnDied -= EnemyDied;
        aliveEnemies--;
        CheckIfAllEnemiesDead();

    }
    private IEnumerator StartGameTimer()
    {
        float time =0;
        while (isGameFinished == false)
        {
            time += Time.deltaTime;
            timer.text = Math.Round(time,2).ToString();
            yield return null;
        }
        gamecompleteTime = (float) Math.Round(time, 2);
        completedStars = HowManyStars();
        Final();

    }
    private void CheckIfAllEnemiesDead()
    {
        if (aliveEnemies<=0)
        {
            LevelCompleted();
        }
    }
    private void Start()
    {
        
        spawner.Spawn(countOfEnemies);
        enemies = spawner.EnemyList;
        allEnemies = enemies.Count;
        aliveEnemies = allEnemies;
        enemies.ForEach(x => x.OnDied += EnemyDied);
        progressBar.Enable(enemies);
        StartCoroutine(StartGameTimer());

    }
    private void LevelCompleted()
    {
        isGameFinished = true;

    }
    private void Final()
    {
        score.Stars = completedStars;
        score.CompleteTime = gamecompleteTime;
        score.EnemiesKilled = allEnemies;
        SetData();
        GameData.SetCompleteLevel(data);
        completed_Screen.Show();
        completed_Screen.Init(score);
    }
    private void SetData()
    {
        data.Id = levelId;
        data.Stars = completedStars;
        data.IsLocked = false;
    }
    private int HowManyStars()
    {
        if(gamecompleteTime == 0)
        {
            return 0;
        }
        if (gamecompleteTime <= mySettings.ThreeStarCompleteTime)
        {
            return 3;
        }
        if (gamecompleteTime <= mySettings.TwoStarCompleteTime && gamecompleteTime > mySettings.ThreeStarCompleteTime)
        {
            return 2;
        }
        if (gamecompleteTime <= mySettings.OneStarCompleteTime && gamecompleteTime > mySettings.TwoStarCompleteTime)
        {
            return 1;
        }
        if (gamecompleteTime <= mySettings.ZeroStarCompleteTime && gamecompleteTime > mySettings.OneStarCompleteTime)
        {
            return 0;
        }
        return 0;
    }
}
