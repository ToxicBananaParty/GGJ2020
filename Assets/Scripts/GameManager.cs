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
    public GameObject player, inspectorPrefab, player2Prefab;
    // Start is called before the first frame update
    void Start()
    {
        //Start timer at level start
        timer = 600.0f;
        assignedColor = AssignColor();
        if (ControlScheme.numPlayers > 1)
            AddPlayer(player2Prefab);

        inspectorWarning = GameObject.Find("Inspector Alert");

        InvokeRepeating("SendInspector", 60, 500);
        InvokeRepeating("InspectorWarning", 5, 500);

        destination = new Vector2(-7, 15);
        offScreenHome = new Vector2(-25, 15);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Round(timer);

        cashText.text = "Cash: " + cash;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddPlayer(player2Prefab);
        }
    }

    private float alertTimer = 0.0f;
    private bool endWarning = false;
    void FixedUpdate()
    {
		if(inspectorWarning != null) {
            if (warning) {
                alertTimer += Time.deltaTime / 2.5f;
                inspectorWarning.transform.position = Vector2.Lerp(offScreenHome, destination, alertTimer);
            }
            if (inspectorWarning.transform.position.x == destination.x) {
                alertTimer = 0.0f;
                warning = false;
                endWarning = true;
            }
            if (endWarning && inspectorWarning.transform.position.x != offScreenHome.x) {
                //Debug.Log("Sending back");
                alertTimer += Time.deltaTime / 2.5f;
                inspectorWarning.transform.position = Vector2.Lerp(destination, offScreenHome, alertTimer);
            }
            if (endWarning && inspectorWarning.transform.position.x == offScreenHome.x) {
                endWarning = false;
            }
        }
    }

    public void AddCash(float profit)
    {
        cash += profit;
    }

    public void AddPlayer(GameObject player2Prefab)
    {
        if (GameObject.Find("Players").transform.childCount < 2)
        {
            Debug.Log("Spawning player!");
            GameObject newPlayer = Instantiate(player2Prefab, GameObject.Find("Players").transform);
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
            //GRAY IS PLACEHOLDER FOR INSPECTOR'S COUNTRY COLOR
        {
            inspector.GetComponent<Dialogue>().FoundWrongFlag();
        }
        else
        {
            inspector.GetComponent<Dialogue>().NothingWrong();
        }
    }

    private GameObject inspectorWarning;
    private Vector2 destination, offScreenHome;
    private bool warning = false;
    void InspectorWarning()
    {
        //Alert player that an inspector will arrive in 30 seconds.

        warning = true;
    }
}
