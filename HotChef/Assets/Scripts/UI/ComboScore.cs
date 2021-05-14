using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ComboScore : MonoBehaviour
{
    float currentCombo;

    TextMeshProUGUI comboText;
    public float maxCombo;
    public float minTextSize, maxTextSize;
    public Gradient gradient;
    public float minimizeRate;
    Vector3 startPos, endPos;

    private void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        comboText.SetText("");
        startPos = transform.position;
        endPos = Vector3.up + startPos;
    }

    public float UpdateCombo(bool isHighSpeed)
    {
        if (!isHighSpeed)
        {
            return 0;
        }
        setText();
        return currentCombo;
    }

    void setText()
    {
        comboText.SetText(String.Format("{0:n0}", currentCombo));
        float value = (currentCombo / maxCombo) ;
        comboText.fontSize = Mathf.Max(minTextSize, value * maxTextSize);
        comboText.color = gradient.Evaluate(value);
        gameObject.SetActive(true);
    }

    public float CheckHighSpeed(bool isHighSpeed)
    {
        if (!isHighSpeed)
        {
            currentCombo = 0;
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ReduceSize());
            }
            return currentCombo;
        }
        currentCombo++;
        return currentCombo;
    }

    public float CurrentCombo
    {
        get { return currentCombo; }
    }

    IEnumerator ReduceSize()
    {
        Vector2 from = Vector3.one;
        Vector3 to = Vector3.one * .25f;
        float elapsed = 0;
        while (elapsed < minimizeRate)
        {
            transform.localScale = Vector3.Lerp(from, to, elapsed / minimizeRate);
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / minimizeRate);
            yield return null;
            elapsed += Time.deltaTime;
        }
        gameObject.SetActive(false);
        transform.localScale = from;
        transform.position = startPos;
    }
}
