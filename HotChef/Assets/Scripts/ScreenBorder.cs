using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorder : MonoBehaviour
{
    Wall[] walls;
    Vector2 screenSize;
    float THICKNESS = 1;
    [SerializeField] Padding paddings;

    private void Start()
    {
        walls = GetComponentsInChildren<Wall>();
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SetupBorders();
    }

    void SetupBorders()
    {
        walls[0].transform.position = new Vector2(screenSize.x + THICKNESS / 2 - paddings.right, 0);
        walls[1].transform.position = new Vector2(-screenSize.x - THICKNESS / 2 + paddings.left, 0);
        walls[2].transform.position = new Vector2(0, screenSize.y + THICKNESS / 2 - paddings.top);
        walls[3].transform.position = new Vector2(0, -screenSize.y - THICKNESS / 2 + paddings.bottom);

        screenSize += screenSize;
        walls[0].SetSize(new Vector2(THICKNESS, screenSize.y));   //right
        walls[1].SetSize(new Vector2(THICKNESS, screenSize.y));   //left
        walls[2].SetSize(new Vector2(screenSize.x, THICKNESS));   //top
        walls[3].SetSize(new Vector2(screenSize.x, THICKNESS));   //bot
    }

    [System.Serializable]
    struct Padding
    {
        public float left;
        public float right;
        public float top;
        public float bottom;
    }

    public void UpdateWalls(bool isHighSpeed, bool isLowSpeed)
    {
        foreach(Wall w in walls)
        {
            w.UpdateWall(isHighSpeed, isLowSpeed);
        }
    }
}
