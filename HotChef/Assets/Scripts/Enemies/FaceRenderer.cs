using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceRenderer : MonoBehaviour
{
    public Sprite[] sprites;
    public float changeRate;
    int currentSprite = 0;
    SpriteRenderer sr;

    protected virtual void Start()
    {
        StartCoroutine(WaitChangeFace(changeRate));
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected IEnumerator WaitChangeFace(float t)
    {
        yield return new WaitForSeconds(t);
        currentSprite = (currentSprite + 1) % sprites.Length;
        ChangeFace(sprites[currentSprite]);
    }

    protected void ChangeFace(Sprite sprite)
    {
        ChangeFace(sprite, changeRate);
    }

    protected void ChangeFace(Sprite sprite, float time)
    {
        StopAllCoroutines();
        sr.sprite = sprite;
        StartCoroutine(WaitChangeFace(time));
    }
}
