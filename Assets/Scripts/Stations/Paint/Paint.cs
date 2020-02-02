using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    private Color toPaint;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Interact()
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
        SetColor(gameObject, toPaint);
    }

    void SetColor(GameObject robotPaintZone, Color color)
    {
        robotPaintZone.GetComponent<SpriteRenderer>().color = color;
    }
}
