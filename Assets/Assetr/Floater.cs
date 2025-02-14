using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f; // Kedalaman sebelum mulai mengapung
    public float displacementAmount = 3f;  // Seberapa besar gaya apung
    public int floaterCount = 1;           // Jumlah titik apung
    public float waterDrag = 0.99f;        // Hambatan di air
    public float waterAngularDrag = 0.5f;  // Hambatan rotasi di air
    public float waveInfluence = 0.5f;     // Seberapa besar rotasi mengikuti ombak

    private void FixedUpdate()
    {
        // Gravitasi tetap diterapkan
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // Ambil tinggi ombak pada posisi kapal
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        // Jika kapal berada di bawah permukaan air
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;

            // Terapkan gaya ke atas agar kapal mengapung
            rigidBody.AddForceAtPosition(Vector3.up * Mathf.Abs(Physics.gravity.y) * displacementMultiplier, transform.position, ForceMode.Acceleration);

            // Tambahkan sedikit efek rotasi dari gelombang (pitch & roll)
            Vector3 waveNormal = WaveManager.instance.GetWaveNormal(transform.position.x);
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, waveNormal) * transform.rotation;
            rigidBody.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, waveInfluence * Time.fixedDeltaTime));

            // Tambahkan sedikit hambatan air
            rigidBody.AddTorque(-rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
