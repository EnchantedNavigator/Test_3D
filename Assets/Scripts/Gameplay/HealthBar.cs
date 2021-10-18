using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField]
    private Slider hpSlider;
    // Start is called before the first frame update
    private void OnEnable()
    {
        hpSlider.value = 1;
        enemy.HealthChanged += Enemy_HealthChanged; 
    }

    private void Enemy_HealthChanged(object sender, float e)
    {
        hpSlider.value = e;
    }

    private void OnDisable()
    {
        enemy.HealthChanged -= Enemy_HealthChanged;
    }
    void Start()
    {
        
    }

}
