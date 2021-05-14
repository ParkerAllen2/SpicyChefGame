using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderValidator : MonoBehaviour
{
    //input
    [SerializeField] bool aliginLeft;
    [SerializeField] float height;
    [SerializeField] float width;
    [Range(0.0f, 1.0f)]
    [SerializeField] float value;

    [SerializeField] float horizontalPadding;
    [SerializeField] float verticalPadding;

    //cached
    float left;
    Vector3 handlePosition;
    Vector3 fillSize;
    protected SpriteRenderer border;
    protected SpriteRenderer background;
    protected SpriteRenderer fill;
    Transform handle;

    Vector3 fillScale;

    protected virtual void Start()
    {
        UpdateWidth();
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }

        UpdateWidth();
        MoveHandle(value);
    }
#endif

    void UpdateWidth()
    {
        //cache
        Transform[] t = GetComponentsInChildren<Transform>();
        border = t[1].GetComponent<SpriteRenderer>();
        background = t[2].GetComponent<SpriteRenderer>();
        fill = t[3].GetComponent<SpriteRenderer>();
        fillScale = t[3].localScale;
        handle = t[4];

        left = -width / 2; 
        Vector3 origin = transform.position;

        //border
        border.size = new Vector2((width + horizontalPadding) / t[1].localScale.x, height + verticalPadding);

        //background
        background.size = new Vector2(width / t[2].localScale.x, height);
        background.transform.position = origin;

        //fill
        fillSize = new Vector2(width, height);
        fill.transform.position = origin;

        //handle
        handlePosition = handle.localPosition;
    }

    public void MoveHandle(float precent)
    {
        float x = width * precent;
        //handle
        handlePosition.x = aliginLeft ? x + left : x / 2;
        handle.localPosition = handlePosition;

        //fill
        fillSize.x = x / fillScale.x;
        fill.size = fillSize;
        if (aliginLeft)
        {
            fill.transform.localPosition = new Vector3(left + x / 2, 0);
        }
    }
}
