using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneController 
{
    public static void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public static void GoToMission(int id)
    {
        GameManager.Instance.SetCurrentSelectIndexMission(id);
        SceneManager.LoadScene(1);

    }

}
