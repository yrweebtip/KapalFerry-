using UnityEngine;
using UnityEngine.UI;

public class FerrySimulation : MonoBehaviour
{
    public GameObject[] carsAtStartPort;  // Mobil di pelabuhan awal
    public GameObject[] holdPointCars;    // Mobil di kapal
    public GameObject[] carsAtEndPort;    // Mobil di pelabuhan tujuan

    public GameObject interactionButton;  // Tombol UI untuk Load/Unload
    private string currentPort = "";      // Menyimpan nama pelabuhan saat player masuk
    private bool isHoldingCar = false;

    void Start()
    {
        if (interactionButton) interactionButton.SetActive(false); // Sembunyikan tombol saat awal
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPort = transform.name; // Simpan nama pelabuhan
            Debug.Log("Player masuk ke: " + currentPort);

            interactionButton.SetActive(true); // Tampilkan tombol

            // Ubah fungsi tombol berdasarkan kondisi
            interactionButton.GetComponent<Button>().onClick.RemoveAllListeners(); // Hapus listener sebelumnya

            if (currentPort == "PelabuhanAwal" && !isHoldingCar)
            {
                interactionButton.GetComponent<Button>().onClick.AddListener(LoadCars);
            }
            else if (currentPort == "PelabuhanTujuan" &! isHoldingCar)
            {
                interactionButton.GetComponent<Button>().onClick.AddListener(UnloadCars);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionButton.SetActive(false); // Sembunyikan tombol saat player keluar dari area
            currentPort = ""; // Reset nama pelabuhan
        }
    }

    void LoadCars()
    {
        isHoldingCar = true;
        Debug.Log("Memulai LoadCars, isHoldingCar: " + isHoldingCar);

        for (int i = 0; i < carsAtStartPort.Length; i++)
        {
            carsAtStartPort[i].SetActive(false);
            holdPointCars[i].SetActive(true);
        }

        interactionButton.SetActive(false); // Sembunyikan tombol setelah aksi
        Debug.Log("Mobil sudah di kapal.");
    }

    void UnloadCars()
    {
        Debug.Log("Menjalankan UnloadCars, isHoldingCar: " + isHoldingCar);

        for (int i = 0; i < holdPointCars.Length; i++)
        {
            holdPointCars[i].SetActive(false);
            carsAtEndPort[i].SetActive(true);
        }

        isHoldingCar = false;
        interactionButton.SetActive(false); // Sembunyikan tombol setelah aksi
        Debug.Log("Semua mobil telah diturunkan.");
    }
}