using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalLocalPos;
    float shakeTime;
    float shakeStrength;

    void Awake()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeTime > 0f)
        {
            transform.localPosition =
                originalLocalPos + Random.insideUnitSphere * shakeStrength;

            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPos,
                Time.deltaTime * 10f
            );
        }
    }

    public void Shake(float duration, float strength)
    {
        shakeTime = duration;
        shakeStrength = strength;
    }
}
