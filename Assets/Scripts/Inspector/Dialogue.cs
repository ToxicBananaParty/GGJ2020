using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text chatBubbleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoundWrongPaint()
    {
        chatBubbleText.text = "My my my! You're fucked!";
        GameObject.Find("Main Camera").GetComponent<GameManager>().cash -= 500;
    }

    public void FoundWrongFlag()
    {
        chatBubbleText.text = "My my my! You're fucked!";
        GameObject.Find("Main Camera").GetComponent<GameManager>().cash -= 1000;
    }

    public void NothingWrong()
    {
        chatBubbleText.text = "Marry me!";
        GameObject.Find("Main Camera").GetComponent<GameManager>().cash += 1000;
    }
}
