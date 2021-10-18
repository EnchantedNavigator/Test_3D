using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Image body;
    [SerializeField]
    private Button myButton;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int currentHp;

    public event System.EventHandler<float> HealthChanged;
    public event System.EventHandler OnDied;
    private void Start()
    {
        HealthChanged?.Invoke(this,currentHp);
    }
    public void TakeDamage(int damage)
    {
        SetHealth(currentHp - damage);

    }
    public void SetHealth(int value) 
    {
        if (value > maxHp)
        {
            currentHp = maxHp;
        }else
        if (value <= maxHp)
        {
            currentHp = value;
        }
        HealthChanged?.Invoke(this,GetNormalizedHp());
        HpCheck();

    }
    public void HpCheck()
    {
        if (currentHp <= 0)
        {
            Die();
        }

    }
    public void Die() 
    {

        OnDied?.Invoke(this, null);
        body.color = Color.gray;
        myButton.interactable = false;

    }
    public float GetNormalizedHp()
    {
        return (float)currentHp / (float)maxHp;
    }

}
