using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorder : MonoBehaviour
{
    BoxCollider2D[] walls;
    Vector2 screenSize;
    float THICKNESS = 1;
    [SerializeField] Padding paddings;

    private void Start()
    {
        walls = GetComponentsInChildren<BoxCollider2D>();
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SetupBorders();
    }

    void SetupBorders()
    {
        walls[0].offset = new Vector2(screenSize.x + THICKNESS / 2 - paddings.right, 0);
        walls[1].offset = new Vector2(-screenSize.x - THICKNESS / 2 + paddings.left, 0);
        walls[2].offset = new Vector2(0, screenSize.y + THICKNESS / 2 - paddings.top);
        walls[3].offset = new Vector2(0, -screenSize.y - THICKNESS / 2 + paddings.bottom);

        screenSize += screenSize;
        walls[0].size = new Vector2(THICKNESS, screenSize.y);
        walls[1].size = new Vector2(THICKNESS, screenSize.y);
        walls[2].size = new Vector2(screenSize.x, THICKNESS);
        walls[3].size = new Vector2(screenSize.x, THICKNESS);
    }

    [System.Serializable]
    struct Padding
    {
        public float left;
        public float right;
        public float top;
        public float bottom;
    }
}
