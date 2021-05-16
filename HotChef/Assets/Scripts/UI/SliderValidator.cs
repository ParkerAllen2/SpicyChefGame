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

    [SerializeField] Padding padding;
    [Range(-1.0f, 1.0f)]
    [SerializeField] float horizontalMargin;
    [Range(-1.0f, 1.0f)]
    [SerializeField] float verticalMargin;



    //cached
    float left;
    Vector3 handlePosition;
    Vector3 fillSize;
    protected SpriteRenderer border;
    protected SpriteRenderer background;
    protected SpriteRenderer fill;
    Transform handle;

    Vector3 fillScale;
    Vector3 screenSize;

    protected virtual void Start()
    {
        UpdateWidth();
        WindowManager.instance.ScreenSizeChangeEvent += Instance_ScreenSizeChangeEvent;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }

        UnityEditor.EditorApplication.delayCall += () =>
        {
            try
            {
                UpdateWidth();
                MoveHandle(value);
            }
            catch { }
        };

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
        
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector3 origin = transform.position = new Vector3(
            screenSize.x * horizontalMargin - left,
            screenSize.y * verticalMargin
            );

        //border
        float w = width + padding.left + padding.right;
        float h = height + padding.top + padding.bottom;
        border.size = new Vector2(w / t[1].localScale.x, h);
        t[1].position = new Vector3((padding.right - padding.left) / 2, (padding.top - padding.bottom) / 2) + origin;

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

    private void Instance_ScreenSizeChangeEvent(int Width, int Height)
    {
        UpdateWidth();
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
