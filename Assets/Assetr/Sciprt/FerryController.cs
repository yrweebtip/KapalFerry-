using UnityEngine;
using UnityEngine.UI;

public class FerryController : MonoBehaviour
{
    [Header("Docking Settings")]
    public Transform dockingPoint;  // Posisi tempat kapal akan merapat
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
            Debug.Log("Kapal berada di dekat: " + currentPort);

            // Tampilkan tombol docking jika belum berlabuh
            if (!isDocked)
            {
                dockingButton.SetActive(true);
                dockingButton.GetComponent<Button>().onClick.RemoveAllListeners();
                dockingButton.GetComponent<Button>().onClick.AddListener(DockShip);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DockingZone") && !isDocked)
        {
            dockingButton.SetActive(false);
            loadUnloadButton.SetActive(false);
            currentPort = "";
        }
    }

    void DockShip()
    {
        Debug.Log("Docking kapal...");

        // Cek lokasi docking dan pindahkan ke docking point yang sesuai
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

        // Aktifkan tali pengaman
        rope.SetActive(true);
        dockingButton.SetActive(false); // Sembunyikan tombol docking setelah docking

        // Tandai kapal sudah berlabuh
        isDocked = true;
        Debug.Log("Kapal berhasil docking di " + currentPort);

        // Tampilkan tombol Load/Unload sesuai pelabuhan
        if (currentPort == "PelabuhanAwal")
        {
            Debug.Log("Menampilkan tombol Load Cars");
            loadUnloadButton.SetActive(true);
            loadUnloadButton.GetComponent<Button>().onClick.RemoveAllListeners();
            loadUnloadButton.GetComponent<Button>().onClick.AddListener(LoadCars);
        }
        else if (currentPort == "PelabuhanTujuan")
        {
            Debug.Log("Menampilkan tombol Unload Cars");
            loadUnloadButton.SetActive(true);
            loadUnloadButton.GetComponent<Button>().onClick.RemoveAllListeners();
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
