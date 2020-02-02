using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetControlScheme(int type)
    {
        ControlScheme.controlType = type;
        SceneManager.LoadScene("_Main");
    }

    public void SetNumPlayers(int num)
    {
        ControlScheme.numPlayers = num;
    }
}

public static class ControlScheme
{
    public static int controlType;
    public static int numPlayers;
}
