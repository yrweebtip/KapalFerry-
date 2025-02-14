using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float turnSpeed = 50f;

    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;

    private bool moveForward, moveBackward, turnLeft, turnRight;

    private void Start()
    {
        AddEventTrigger(forwardButton, EventTriggerType.PointerDown, () => moveForward = true);
        AddEventTrigger(forwardButton, EventTriggerType.PointerUp, () => moveForward = false);

        AddEventTrigger(backwardButton, EventTriggerType.PointerDown, () => moveBackward = true);
        AddEventTrigger(backwardButton, EventTriggerType.PointerUp, () => moveBackward = false);

        AddEventTrigger(leftButton, EventTriggerType.PointerDown, () => turnLeft = true);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, () => turnLeft = false);

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, () => turnRight = true);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, () => turnRight = false);
    }

    private void Update()
    {
        if (moveForward) transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        if (moveBackward) transform.Translate(-Vector3.forward * forwardSpeed * Time.deltaTime);
        if (turnLeft) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if (turnRight) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    void AddEventTrigger(Button button, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}
