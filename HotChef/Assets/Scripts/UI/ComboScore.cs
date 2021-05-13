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

    private void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        comboText.SetText("");
    }

    public float UpdateCombo(bool isHighSpeed)
    {
        if (!isHighSpeed)
        {
            return 0;
        }
        currentCombo++;
        setText();
        return currentCombo;
    }

    void setText()
    {
        comboText.SetText(String.Format("{0:n0}", currentCombo));
        float value = (currentCombo / maxCombo) ;
        comboText.fontSize = Mathf.Min(value + minTextSize, maxTextSize);
        comboText.color = gradient.Evaluate(value);
    }

    public void CheckHighSpeed(bool isHighSpeed)
    {
        if (!isHighSpeed)
        {
            currentCombo = 1;
            comboText.SetText("");
        }
    }

    public float CurrentCombo
    {
        get { return currentCombo; }
    }
}
