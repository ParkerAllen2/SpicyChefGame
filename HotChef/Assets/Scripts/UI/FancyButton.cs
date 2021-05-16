using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyButton : MonoBehaviour
{
    public float shiftRate;
    Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void HorizontalShift(float distance)
    {
        StopAllCoroutines();
        Vector3 to = new Vector3(originalPosition.x + distance, originalPosition.y);
        StartCoroutine(Shift(to));
    }

    public void VerticalShift(float distance)
    {
        StopAllCoroutines();
        Vector3 to = new Vector3(originalPosition.x, originalPosition.y + distance);
        StartCoroutine(Shift(to));
    }

    public void ResetShift()
    {
        StopAllCoroutines();
        StartCoroutine(Shift(originalPosition));
    }

    IEnumerator Shift(Vector3 to)
    {
        Vector2 from = transform.position;
        float elapsed = 0;
        while (elapsed < shiftRate)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / shiftRate);
            yield return null;
            elapsed += Time.deltaTime;
        }
        transform.position = to;
    }
}
