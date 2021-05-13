using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    Light2D globalColorLight;
    public float maxIntensity;
    public Gradient colorGradient;

    private void Start()
    {
        globalColorLight = GetComponentInChildren<Light2D>();
    }

    public void UpdateGlobalLight(float velocity)
    {
        Color color = colorGradient.Evaluate(velocity);
        globalColorLight.color = color;
        globalColorLight.intensity = color.a;
    }
}
