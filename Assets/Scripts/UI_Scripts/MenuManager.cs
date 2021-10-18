using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Base_Screen> _screens;
    public static MenuManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _screens.ForEach(x => x.MyManager = this);
    }
    public void OpenPanel(ScreenType screenType)
    {
       Base_Screen screenFound = _screens.Find(x => x.Type == screenType);
        screenFound.Show();
    }
    public void ClosePanel(Base_Screen screen)
    {
        screen.Hide();
    }
}
