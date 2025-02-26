using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float floatStrength = 1.0f; // Seberapa cepat kapal menyesuaikan tinggi air
    public float rotationDamping = 2.0f; // Seberapa halus kapal berayun mengikuti gelombang
    public float boatHeightOffset = 1.5f; // Offset tinggi agar kapal tidak tenggelam

    private Rigidbody rb;
    private WaveManager waveManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waveManager = WaveManager.instance;
    }

    void Update()
    {
        if (waveManager == null) return;

        // Dapatkan tinggi gelombang berdasarkan posisi X kapal
        float waveHeight = waveManager.GetWaveHeight(transform.position.x);

        // Sesuaikan posisi Y kapal agar tetap berada di atas permukaan air
        Vector3 targetPosition = new Vector3(transform.position.x, waveHeight + boatHeightOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * floatStrength);

        // Miringkan kapal mengikuti kemiringan gelombang
        Vector3 waveNormal = waveManager.GetWaveNormal(transform.position.x);
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, waveNormal) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }
}