using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Chase Settings")]
    public float distance = 5.5f;   // arkadan mesafe
    public float height = 1.8f;      // kanat üstü
    public float lookAhead = 3f;     // ileri bakýþ

    [Header("Smooth")]
    public float positionSmooth = 10f;
    public float rotationSmooth = 10f;

    Vector3 velocity;

    void LateUpdate()
    {
        if (!target) return;

        // Arabanýn ileri yönü (SADECE Y düzleminde)
        Vector3 forwardFlat = Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized;

        // Kamera pozisyonu: arkadan + yukarý
        Vector3 desiredPos =
            target.position
            - forwardFlat * distance
            + Vector3.up * height;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            1f / positionSmooth
        );

        // Kamera bakýþý: ileri doðru (roll YOK)
        Vector3 lookTarget = target.position + forwardFlat * lookAhead;

        Quaternion desiredRot = Quaternion.LookRotation(
            lookTarget - transform.position,
            Vector3.up
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRot,
            rotationSmooth * Time.deltaTime
        );
    }
}
