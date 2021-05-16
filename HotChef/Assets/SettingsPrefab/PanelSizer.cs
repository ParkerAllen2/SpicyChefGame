using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSizer : MonoBehaviour
{
    public float showSpeed;
    public bool startHorizontalZero;
    public bool startVerticalZero;
    RectTransform panel;
    Vector2 defaultSize;
    Vector2 current;
    Vector2 to;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        defaultSize = panel.sizeDelta;
        current = defaultSize;
        if (startHorizontalZero)
            current.x = 0;
        if (startVerticalZero)
            current.y = 0;

        to = defaultSize;
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, current.x);
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, current.y);
        gameObject.SetActive(current.x * current.y != 0);
    }

    public void PanelShow()
    {
        gameObject.SetActive(true);
        to = defaultSize;
        StopAllCoroutines();
        StartCoroutine(ChangeSize(true));
    }

    public void PanelWidth(float width)
    {
        panel.gameObject.SetActive(true);
        to.x = width;
        StopAllCoroutines();
        StartCoroutine(ChangeSize(true));
    }

    public void PanelHeight(float height)
    {
        panel.gameObject.SetActive(true);
        to.y = height;
        StopAllCoroutines();
        StartCoroutine(ChangeSize(true));
    }

    public void PanelHide()
    {
        to = Vector2.zero;
        StopAllCoroutines();
        StartCoroutine(ChangeSize(false));
    }

    IEnumerator ChangeSize(bool active)
    {
        Vector2 from = current;
        float t = 0;
        while (t < showSpeed)
        {
            current = Vector2.Lerp(from, to, t / showSpeed);
            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, current.x);
            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, current.y);
            yield return null;
            t += Time.deltaTime;
        }

        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, to.x);
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, to.y);
        panel.gameObject.SetActive(active);
    }
}
