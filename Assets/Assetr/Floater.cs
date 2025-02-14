using UnityEngine;

public class Floater : MonoBehaviour
{
    public float floatHeight = 2f;
    public float bounceDamp = 0.05f;
    public float waterLevel = 0f;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float waveHeight = GetWaveHeightAtPosition(pos);

        if (pos.y < waveHeight)
        {
            float buoyancyForce = (waveHeight - pos.y) * floatHeight - GetComponent<Rigidbody>().linearVelocity.y * bounceDamp;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, buoyancyForce, 0), ForceMode.Acceleration);
        }
    }

    float GetWaveHeightAtPosition(Vector3 position)
    {
        // Ambil data tinggi ombak dari sistem Dynamic Wave
        return Mathf.Sin(Time.time + position.x) + waterLevel; // Contoh ombak sederhana
    }
}
