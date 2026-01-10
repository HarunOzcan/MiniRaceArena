using UnityEngine;

public class WheelSteerVisual : MonoBehaviour
{
    [Header("Steering Settings")]
    public float maxSteerAngle = 30f;

    float steerInput;

    void Update()
    {
        // Klavye inputu (A / D)
        steerInput = Input.GetAxisRaw("Horizontal");

        // Hedef açý
        float targetAngle = steerInput * maxSteerAngle;

        // Sadece Y ekseninde döndür
        Quaternion targetRotation = Quaternion.Euler(
            transform.localEulerAngles.x,
            targetAngle,
            transform.localEulerAngles.z
        );

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * 10f
        );
    }
}
