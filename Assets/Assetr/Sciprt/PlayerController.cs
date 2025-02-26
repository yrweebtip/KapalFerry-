using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipMovement : MonoBehaviour
{
    public float forwardSpeed = 5f; // Kecepatan maju/mundur
    public float turnSpeed = 50f;   // Kecepatan rotasi kiri/kanan
    public float horizontalSpeed = 5f; // Kecepatan gerakan horizontal (kiri/kanan)

    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public Button strafeLeftButton; // Tombol untuk gerakan horizontal kiri
    public Button strafeRightButton; // Tombol untuk gerakan horizontal kanan

    private bool moveForward, moveBackward, turnLeft, turnRight, strafeLeft, strafeRight;

    private void Start()
    {
        // Setup tombol maju/mundur
        AddEventTrigger(forwardButton, EventTriggerType.PointerDown, () => moveForward = true);
        AddEventTrigger(forwardButton, EventTriggerType.PointerUp, () => moveForward = false);

        AddEventTrigger(backwardButton, EventTriggerType.PointerDown, () => moveBackward = true);
        AddEventTrigger(backwardButton, EventTriggerType.PointerUp, () => moveBackward = false);

        // Setup tombol rotasi kiri/kanan
        AddEventTrigger(leftButton, EventTriggerType.PointerDown, () => turnLeft = true);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, () => turnLeft = false);

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, () => turnRight = true);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, () => turnRight = false);

        // Setup tombol gerakan horizontal kiri/kanan
        AddEventTrigger(strafeLeftButton, EventTriggerType.PointerDown, () => strafeLeft = true);
        AddEventTrigger(strafeLeftButton, EventTriggerType.PointerUp, () => strafeLeft = false);

        AddEventTrigger(strafeRightButton, EventTriggerType.PointerDown, () => strafeRight = true);
        AddEventTrigger(strafeRightButton, EventTriggerType.PointerUp, () => strafeRight = false);
    }

    private void Update()
    {
        // Gerakan maju/mundur
        if (moveForward) transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        if (moveBackward) transform.Translate(-Vector3.forward * forwardSpeed * Time.deltaTime);

        // Rotasi kiri/kanan
        if (turnLeft) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if (turnRight) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        // Gerakan horizontal (strafe) kiri/kanan
        if (strafeLeft) transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime, Space.Self);
        if (strafeRight) transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime, Space.Self);
    }

    void AddEventTrigger(Button button, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}