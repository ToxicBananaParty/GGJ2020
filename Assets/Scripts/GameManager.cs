using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;

public class GameManager : MonoBehaviour
{
    public float timer = 0.0f;
    public float cash = 500.0f;
    public float inspectionChance = 0.0f;
    public Text timerText;
    public Text cashText;
    public GameObject player;
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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddPlayer(player);
        }
    }

    public void AddCash(float profit)
    {
        cash += profit;
    }

    public void AddPlayer(GameObject playerPrefab)
    {
        if (GameObject.Find("Players").transform.childCount < 2)
        {
            Debug.Log("Spawning player!");
            GameObject newPlayer = Instantiate(playerPrefab, GameObject.Find("Players").transform);
        }
    }

    public void DropPlayer(string playerName)
    {
        GameObject.Destroy(GameObject.Find(playerName));
    }
}
