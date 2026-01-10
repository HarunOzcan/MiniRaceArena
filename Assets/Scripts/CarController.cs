using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float motorForce = 1500f;
    public float maxSpeed = 20f;
    public float turnSpeed = 80f;

    [Header("Braking")]
    public float brakeForce = 3000f;

    [Header("Handbrake")]
    public float normalDamping = 0.1f;
    public float handbrakeDamping = 2.5f;

    [Header("Grip (Side Slip Control)")]
    public float sideGrip = 8f;
    public float handbrakeSideGrip = 2f;

    [Header("Collision Shake")]
    public float minImpactToShake = 2f;

    Rigidbody rb;
    CameraShake cameraShake;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        rb.linearDamping = normalDamping;

        if (Camera.main != null)
            cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");        // W / S
        float horizontal = Input.GetAxisRaw("Horizontal");    // A / D
        bool handbrake = Input.GetKey(KeyCode.Space);         // SPACE

        /* -------- GAZ -------- */
        if (vertical > 0 && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(
                transform.forward * vertical * motorForce * Time.fixedDeltaTime,
                ForceMode.Acceleration
            );
        }

        /* -------- NORMAL FREN -------- */
        if (vertical < 0 && rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 brakeDir = -rb.linearVelocity.normalized;
            rb.AddForce(
                brakeDir * brakeForce * Time.fixedDeltaTime,
                ForceMode.Acceleration
            );
        }

        /* -------- DÖNÜŞ -------- */
        if (rb.linearVelocity.magnitude > 0.5f && Mathf.Abs(horizontal) > 0.01f)
        {
            float turn = horizontal * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0f, turn, 0f);
        }

        /* -------- EL FRENİ -------- */
        rb.linearDamping = handbrake ? handbrakeDamping : normalDamping;

        /* -------- YAN KAYMA KONTROLÜ -------- */
        ApplySideGrip(handbrake);
    }

    void ApplySideGrip(bool handbrake)
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

        float grip = handbrake ? handbrakeSideGrip : sideGrip;

        localVelocity.x = Mathf.Lerp(
            localVelocity.x,
            0f,
            grip * Time.fixedDeltaTime
        );

        rb.linearVelocity = transform.TransformDirection(localVelocity);
    }

    /* -------- ÇARPIŞMA → KAMERA SHAKE -------- */
    void OnCollisionEnter(Collision collision)
    {
        if (cameraShake == null) return;

        float impact = collision.relativeVelocity.magnitude;
        if (impact < minImpactToShake) return;

        float duration = Mathf.Clamp(impact * 0.05f, 0.1f, 0.3f);
        float strength = Mathf.Clamp(impact * 0.03f, 0.05f, 0.3f);

        cameraShake.Shake(duration, strength);
    }
}
