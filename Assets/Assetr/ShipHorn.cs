using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipHorn : MonoBehaviour
{
    public Button hornButton; // Tombol untuk membunyikan horn
    public AudioSource hornAudioSource; // Audio Source untuk memainkan suara horn

    private void Start()
    {
        // Pastikan tombol horn dan audio source sudah di-set
        if (hornButton == null)
        {
            Debug.LogError("Tombol horn belum di-set di Inspector!");
            return;
        }

        if (hornAudioSource == null)
        {
            Debug.LogError("Audio Source belum di-set di Inspector!");
            return;
        }

        // Tambahkan event trigger untuk tombol horn
        AddEventTrigger(hornButton, EventTriggerType.PointerDown, PlayHornSound);
    }

    void PlayHornSound()
    {
        // Memainkan suara horn jika audio source ada dan tidak sedang memainkan suara
        if (hornAudioSource != null && !hornAudioSource.isPlaying)
        {
            hornAudioSource.Play();
        }
    }

    void AddEventTrigger(Button button, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}