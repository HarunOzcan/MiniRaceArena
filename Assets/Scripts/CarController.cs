using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Player")]
    public int playerID = 0; // 0=P1, 1=P2, 2=P3, 3=P4

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
        float vertical = 0f;
        float horizontal = 0f;
        bool handbrake = false;

        // -------- PLAYER INPUT AYIRMA --------
        if (playerID == 0) // Player 1 → WASD
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
            handbrake = Input.GetKey(KeyCode.Space);
        }
        else if (playerID == 1) // Player 2 → I J K L
        {
            vertical = (Input.GetKey(KeyCode.I) ? 1 : 0) - (Input.GetKey(KeyCode.K) ? 1 : 0);
            horizontal = (Input.GetKey(KeyCode.L) ? 1 : 0) - (Input.GetKey(KeyCode.J) ? 1 : 0);
            handbrake = Input.GetKey(KeyCode.RightShift);
        }
        else if (playerID == 2) // Player 3 → Arrow Keys
        {
            vertical = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);
            horizontal = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
            handbrake = Input.GetKey(KeyCode.RightControl);
        }
        else if (playerID == 3) // Player 4 → Numpad
        {
            vertical = (Input.GetKey(KeyCode.Keypad8) ? 1 : 0) - (Input.GetKey(KeyCode.Keypad5) ? 1 : 0);
            horizontal = (Input.GetKey(KeyCode.Keypad6) ? 1 : 0) - (Input.GetKey(KeyCode.Keypad4) ? 1 : 0);
            handbrake = Input.GetKey(KeyCode.Keypad0);
        }

        /* -------- GAZ -------- */
        if (vertical > 0 && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * vertical * motorForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        /* -------- FREN -------- */
        if (vertical < 0 && rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 brakeDir = -rb.linearVelocity.normalized;
            rb.AddForce(brakeDir * brakeForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        /* -------- DÖNÜŞ -------- */
        if (rb.linearVelocity.magnitude > 0.5f && Mathf.Abs(horizontal) > 0.01f)
        {
            float turn = horizontal * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0f, turn, 0f);
        }

        /* -------- EL FRENİ -------- */
        rb.linearDamping = handbrake ? handbrakeDamping : normalDamping;

        /* -------- YAN KAYMA -------- */
        ApplySideGrip(handbrake);
    }

    void ApplySideGrip(bool handbrake)
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        float grip = handbrake ? handbrakeSideGrip : sideGrip;

        localVelocity.x = Mathf.Lerp(localVelocity.x, 0f, grip * Time.fixedDeltaTime);
        rb.linearVelocity = transform.TransformDirection(localVelocity);
    }

    /* -------- ÇARPIŞMA SHAKE -------- */
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
