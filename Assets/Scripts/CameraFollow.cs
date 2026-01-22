using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Chase Settings")]
    public float distance = 5.5f;
    public float height = 1.8f;
    public float lookAhead = 3f;

    [Header("Smooth")]
    public float positionSmooth = 8f;
    public float rotationSmooth = 8f;

    Vector3 velocity;

    void LateUpdate()
    {
        if (target == null)
        {
            // Eðer target yoksa Player1 arabasýný otomatik bul
            GameObject playerCar = GameObject.FindWithTag("Player");
            if (playerCar != null)
                target = playerCar.transform;
            else
                return;
        }

        // Arabanýn ileri yönü (Y ekseni düz)
        Vector3 forwardFlat = Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized;

        // Kamera pozisyonu
        Vector3 desiredPos = target.position - forwardFlat * distance + Vector3.up * height;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            1f / positionSmooth
        );

        // Kamera bakýþ noktasý
        Vector3 lookTarget = target.position + forwardFlat * lookAhead;

        Quaternion desiredRot = Quaternion.LookRotation(lookTarget - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRot,
            rotationSmooth * Time.deltaTime
        );
    }
}
