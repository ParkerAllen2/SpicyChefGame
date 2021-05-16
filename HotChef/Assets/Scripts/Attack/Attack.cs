using UnityEngine;

[System.Serializable]
public class Attack
{
    public string animationName;
    public float magnitude;
    public float swingTime;
    public float stunTime;
    public float offsetAngle;
    public float attackRate;
    float nextUseTime;
    public string sound;

    public void SetCooldown()
    {
        nextUseTime = Time.time + attackRate;
    }

    public bool CanUse()
    {
        return nextUseTime < Time.time;
    }
}
