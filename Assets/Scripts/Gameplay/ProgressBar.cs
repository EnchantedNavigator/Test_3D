using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Slider progressSlider;
    private List<Enemy> enemies;
    private int allEnemies;
    private int aliveEnemies;

    private void EnemyDied(object sender, System.EventArgs e)
    {
        Enemy enemy = sender as Enemy;
        enemy.OnDied -= EnemyDied;
        aliveEnemies--;
        progressSlider.value = ((float)aliveEnemies / (float)allEnemies);
    }
    public void Enable(List<Enemy> _enemies)
    {
        enemies = _enemies;
        progressSlider.value = 1;
        allEnemies = enemies.Count;
        aliveEnemies = allEnemies;
        enemies.ForEach(x => x.OnDied += EnemyDied);
    }
    private void OnDisable()
    {
        enemies.ForEach(x => x.OnDied -= EnemyDied);
    }
}
