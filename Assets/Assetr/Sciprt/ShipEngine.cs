using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipEngineWithButtons : MonoBehaviour
{
    public Button engineButton; // Tombol untuk menyalakan/mematikan engine
    public AudioSource engineAudioSource; // Audio Source untuk suara engine

    public GameObject movementButtonsPanel; // Panel UI yang berisi tombol-tombol movement

    private bool isEngineOn = false; // Status engine (nyala/mati)

    private void Start()
    {
        // Pastikan tombol engine, audio source, dan panel tombol movement sudah di-set
        if (engineButton == null)
        {
            Debug.LogError("Tombol engine belum di-set di Inspector!");
            return;
        }

        if (engineAudioSource == null)
        {
            Debug.LogError("Audio Source belum di-set di Inspector!");
            return;
        }

        if (movementButtonsPanel == null)
        {
            Debug.LogError("Panel tombol movement belum di-set di Inspector!");
            return;
        }

        // Sembunyikan tombol movement di awal
        movementButtonsPanel.SetActive(false);

        // Tambahkan event trigger untuk tombol engine
        AddEventTrigger(engineButton, EventTriggerType.PointerDown, ToggleEngine);
    }

    void ToggleEngine()
    {
        // Toggle status engine
        isEngineOn = !isEngineOn;

        // Memainkan atau menghentikan suara engine
        if (isEngineOn)
        {
            if (engineAudioSource != null && !engineAudioSource.isPlaying)
            {
                engineAudioSource.Play();
            }
        }
        else
        {
            if (engineAudioSource != null && engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }

        // Tampilkan atau sembunyikan tombol movement berdasarkan status engine
        movementButtonsPanel.SetActive(isEngineOn);

        Debug.Log("Engine is " + (isEngineOn ? "ON" : "OFF"));
    }

    void AddEventTrigger(Button button, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}