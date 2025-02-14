using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5f;   // Seberapa cepat kapal bergerak
    public float maxSpeed = 4f;       // Kecepatan maksimum kapal
    public float turnSpeed = 40f;     // Seberapa cepat kapal berbelok
    public float turnDamping = 2f;    // Efek untuk mengurangi putaran berlebih

    private Rigidbody rb;
    private float moveInput = 0f;
    private float turnInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0.5f;               // Sedikit drag untuk mengurangi meluncur
        rb.angularDamping = 2f;          // Mencegah kapal terus berputar
    }

    void FixedUpdate()
    {
        // Tambahkan gaya maju hanya jika ada input
        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        // Batasi kecepatan maksimum kapal
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Belok dengan lebih smooth
        if (turnInput != 0)
        {
            rb.AddTorque(Vector3.up * turnInput * turnSpeed, ForceMode.Acceleration);
        }

        // Stabilkan rotasi agar tidak terus berputar
        rb.angularVelocity *= (1f - Time.fixedDeltaTime * turnDamping);
    }

    // Fungsi untuk mengontrol tombol D-Pad
    public void MoveForward() { moveInput = 1f; }
    public void MoveBackward() { moveInput = -1f; }
    public void StopMoving() { moveInput = 0f; }

    public void TurnLeft() { turnInput = -1f; }
    public void TurnRight() { turnInput = 1f; }
    public void StopTurning() { turnInput = 0f; }
}
