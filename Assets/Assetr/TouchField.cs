using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 touchDist;

    [HideInInspector]
    public bool pressed;

    private Vector2 pointerOld;
    private int pointerId;
    private bool isMouse;

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (isMouse)
            {
                touchDist = (Vector2)Input.mousePosition - pointerOld;
                pointerOld = Input.mousePosition;
            }
            else if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                touchDist = Input.touches[pointerId].position - pointerOld;
                pointerOld = Input.touches[pointerId].position;
            }
        }
        else
        {
            touchDist = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;

        // Tentukan apakah input berasal dari mouse atau layar sentuh
        isMouse = eventData.pointerId == -1; // Pointer ID -1 menunjukkan mouse
        Debug.Log($"Pointer down at position: {eventData.position}, isMouse: {isMouse}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        Debug.Log($"Pointer up at position: {eventData.position}");
    }
}
