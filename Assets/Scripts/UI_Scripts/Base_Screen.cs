using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType { Main,Mission,Game,Completed};
public abstract class Base_Screen : MonoBehaviour
{
    [SerializeField]
    protected GameObject MyPannel;
    public MenuManager MyManager;
    [SerializeField] private string key;
    [SerializeField] ScreenType type;

    public string Key { get => key; }
    public ScreenType Type { get => type;  }

    public abstract void Show();
    public abstract void Hide();

}
