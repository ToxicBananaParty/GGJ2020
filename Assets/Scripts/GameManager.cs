using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public float timer = 0.0f;
    public float cash = 500.0f;
    public float inspectionChance = 0.0f;
    public Text timerText;
    public Text cashText;
    public Color toPaint, assignedColor;
    public GameObject player, inspectorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //Start timer at level start
        timer = 600.0f;
        assignedColor = AssignColor();
        if (ControlScheme.numPlayers > 1)
            AddPlayer(player);

        InvokeRepeating("SendInspector", 60, 500);
        InvokeRepeating("InspectorWarning", 30, 500);
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

    public Color AssignColor()
    {
        //Paint random color
        int selection = Random.Range(0, 5);
        switch (selection)
        {
            case 1:
                toPaint = Color.yellow;
                break;
            case 2:
                toPaint = Color.green;
                break;
            case 3:
                toPaint = Color.red;
                break;
            case 4:
                toPaint = Color.blue;
                break;
            default:
                toPaint = Color.blue;
                break;
        }
        SetColor(GameObject.Find("Canvas/Assignment/Image"), toPaint);
        return toPaint;
    }

    void SetColor(GameObject robotPaintZone, Color color)
    {
		if(robotPaintZone != null) {
            robotPaintZone.GetComponent<Image>().color = color;
        }
    }

    void SendInspector()
    {
        GameObject inspector = Instantiate(inspectorPrefab, new Vector3(-15.0f, -3.0f, 0.0f), Quaternion.identity);
        if (GameObject.Find("Paint Station").GetComponent<PaintButtonInteraction>().currentColor != Color.gray)
            //GRAY IS PLACEHOLDER FOR INSPECTOR'S COLOR
        {
            //Fine for missing flags
        }
        else
        {
            //Award for correct flags
        }
    }

    void InspectorWarning()
    {
        //Alert player that an inspector will arrive in 30 seconds.
        GameObject.Find("Inspector Alert").GetComponent<Animator>().SetTrigger("Alert");
    }
}
