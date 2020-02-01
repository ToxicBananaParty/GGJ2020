using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timer = 0.0f;
    public float cash = 0.0f;
    public Text timerText;
    public Text cashText;
    // Start is called before the first frame update
    void Start()
    {
        //Start timer at level start
        timer = 600.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Round(timer);

        cashText.text = "Cash: " + cash;
    }

    public void AddCash(float profit)
    {
        cash += profit;
    }
}
