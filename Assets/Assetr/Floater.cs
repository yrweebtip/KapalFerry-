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
    public float stabilityForce = 5f;      // Gaya untuk menstabilkan kapal

    private void FixedUpdate()
    {
        // Ambil tinggi ombak pada posisi kapal
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        // Terapkan gaya gravitasi seperti biasa
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // Jika kapal berada di bawah permukaan air
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;

            // Terapkan gaya ke atas agar kapal mengapung
            rigidBody.AddForceAtPosition(Vector3.up * Mathf.Abs(Physics.gravity.y) * displacementMultiplier, transform.position, ForceMode.Acceleration);

            // Ambil normal ombak untuk menentukan kemiringan
            Vector3 waveNormal = WaveManager.instance.GetWaveNormal(transform.position.x);

            // Stabilkan kapal agar tidak terbawa horizontal wave
            Vector3 targetUp = Vector3.Lerp(transform.up, waveNormal, waveInfluence * Time.fixedDeltaTime);
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, targetUp) * transform.rotation;
            rigidBody.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, stabilityForce * Time.fixedDeltaTime));

            // Hilangkan efek arus air dengan hanya mempertahankan kecepatan horizontal kapal
            Vector3 horizontalVelocity = new Vector3(rigidBody.linearVelocity.x, 0, rigidBody.linearVelocity.z);
            rigidBody.linearVelocity = horizontalVelocity; // Menghapus dorongan vertikal akibat ombak
        }
    }
}
