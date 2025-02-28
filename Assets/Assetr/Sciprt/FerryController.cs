using UnityEngine;
using UnityEngine.UI;

public class FerryController : MonoBehaviour
{
    [Header("Docking Settings")]
    public GameObject dockingButton; // Tombol untuk docking
    public GameObject rope; // Tali pengaman yang muncul setelah docking
    public Transform dockingPointStart;  // Docking point di pelabuhan awal
    public Transform dockingPointEnd;    // Docking point di pelabuhan tujuan


    [Header("Car Management")]
    public GameObject[] carsAtStartPort;  // Mobil di pelabuhan awal
    public GameObject[] holdPointCars;    // Mobil di kapal
    public GameObject[] carsAtEndPort;    // Mobil di pelabuhan tujuan
    public GameObject loadUnloadButton;   // Tombol Load/Unload mobil

    private bool isDocked = false; // Apakah kapal sudah berlabuh?
    private string currentPort = ""; // Pelabuhan saat ini
    private bool isHoldingCar = false; // Apakah kapal membawa mobil?
   


    void Start()
    {
        dockingButton.SetActive(false);
        rope.SetActive(false);
        loadUnloadButton.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DockingZone"))
        {
            currentPort = other.name; // Simpan nama pelabuhan
            Debug.Log("Kapal berada di: " + currentPort);

            dockingButton.SetActive(true); // Tampilkan tombol docking

            dockingButton.GetComponent<Button>().onClick.RemoveAllListeners();
            dockingButton.GetComponent<Button>().onClick.AddListener(DockShip);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DockingZone"))
        {
            Debug.Log("Kapal meninggalkan: " + currentPort);

            dockingButton.SetActive(false); // Sembunyikan tombol docking

            // ✅ Reset status docking saat kapal meninggalkan pelabuhan
            isDocked = false;
        }
    }



    void DockShip()
    {
        Debug.Log("Docking kapal...");

        // Pastikan docking hanya terjadi jika belum docking di tempat yang sama
        if (isDocked)
        {
            Debug.Log("Kapal sudah docking sebelumnya, tidak bisa docking lagi di lokasi yang sama.");
            return;
        }

        // Tentukan posisi docking berdasarkan pelabuhan saat ini
        if (currentPort == "PelabuhanAwal")
        {
            transform.position = dockingPointStart.position;
            transform.rotation = dockingPointStart.rotation;
        }
        else if (currentPort == "PelabuhanTujuan")
        {
            transform.position = dockingPointEnd.position;
            transform.rotation = dockingPointEnd.rotation;
        }
        else
        {
            Debug.Log("ERROR: Docking gagal, currentPort tidak valid!");
            return;
        }

        // Aktifkan tali dan sembunyikan tombol docking
        rope.SetActive(true);
        dockingButton.SetActive(false);

        // Tandai kapal sudah docking
        isDocked = true;

        Debug.Log("Kapal berhasil docking di " + currentPort);

        // Tampilkan tombol Load/Unload jika sesuai
        loadUnloadButton.SetActive(true);
        loadUnloadButton.GetComponent<Button>().onClick.RemoveAllListeners();

        if (currentPort == "PelabuhanAwal")
        {
            loadUnloadButton.GetComponent<Button>().onClick.AddListener(LoadCars);
        }
        else if (currentPort == "PelabuhanTujuan")
        {
            loadUnloadButton.GetComponent<Button>().onClick.AddListener(UnloadCars);
        }
    }




    void HandleCarTransfer()
    {
        if (currentPort == "PelabuhanAwal" && !isHoldingCar)
        {
            LoadCars();
        }
        else if (currentPort == "PelabuhanTujuan" && isHoldingCar)
        {
            UnloadCars();
        }
    }

    void LoadCars()
    {
        Debug.Log("Memuat mobil ke kapal...");

        for (int i = 0; i < carsAtStartPort.Length; i++)
        {
            carsAtStartPort[i].SetActive(false);
            holdPointCars[i].SetActive(true);
        }

        isHoldingCar = true; // Tetap tandai kapal membawa mobil
        loadUnloadButton.SetActive(false); // Sembunyikan tombol setelah load
        Debug.Log("Mobil sudah berada di kapal.");
    }

    void UnloadCars()
    {
        Debug.Log("Menurunkan mobil di pelabuhan tujuan...");

        for (int i = 0; i < holdPointCars.Length; i++)
        {
            holdPointCars[i].SetActive(false);
            carsAtEndPort[i].SetActive(true);
        }

        isHoldingCar = false; // Reset status kapal
        loadUnloadButton.SetActive(false); // Sembunyikan tombol setelah unload
        Debug.Log("Semua mobil telah diturunkan.");
    }
}
