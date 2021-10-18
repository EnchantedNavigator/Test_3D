using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject container;
    private List<int> usedSpawnPoints = new List<int>();
    private List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList { get => enemyList; }

    private int myCountOfEnemies;
    public void Spawn(int countOfEnemies)
    {
        myCountOfEnemies = countOfEnemies;
        if (myCountOfEnemies > _spawnPoints.Count())
        {
            myCountOfEnemies = _spawnPoints.Count();
        }
        for(int i = 0; i < myCountOfEnemies; i++)
        {
            Enemy spawned = Instantiate(prefab, container.transform);
            Image spawnedImage = spawned.GetComponent<Image>();
            Color randomColor = Color.green;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    randomColor = Color.black;
                    break;
                case 1:
                    randomColor = Color.red;
                    break;
                case 2:
                    randomColor = Color.blue;
                    break;
                case 3:
                    randomColor = Color.yellow;
                    break;
            }

            spawnedImage.color = randomColor;
            int spawnPointNumber=0;
            bool isOkay=false;
            while (isOkay==false) { 
                spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                
                if (usedSpawnPoints.Contains(spawnPointNumber)==false) 
                {
                    usedSpawnPoints.Add(spawnPointNumber);
                    break;
                }
            }
            SetEnemy(spawned, _spawnPoints[spawnPointNumber].position);
            enemyList.Add(spawned);
        }
    } 
    private void SetEnemy(Enemy enemy,Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
