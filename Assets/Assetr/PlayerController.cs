using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5f;   // Seberapa cepat kapal bergerak
    public float maxSpeed = 4f;       // Kecepatan maksimum kapal
    public float turnSpeed = 40f;     // Seberapa cepat kapal berbelok

    private Rigidbody rb;
    private float moveInput = 0f;
    private float turnInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f;  // Hapus efek drag
        rb.angularDamping = 0f;  // Hapus efek rotasi lambat
    }

    void FixedUpdate()
    {
        // Tambahkan gaya ke depan hanya jika ada input gerakan
        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        // Batasi kecepatan maksimum kapal
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Rotasi kapal lebih smooth dengan AddTorque
        rb.AddTorque(Vector3.up * turnInput * turnSpeed, ForceMode.Acceleration);
    }

    // Fungsi untuk mengontrol tombol D-Pad
    public void MoveForward() { moveInput = 1f; }
    public void MoveBackward() { moveInput = -1f; }
    public void StopMoving() { moveInput = 0f; }

    public void TurnLeft() { turnInput = -1f; }
    public void TurnRight() { turnInput = 1f; }
    public void StopTurning() { turnInput = 0f; }
}
