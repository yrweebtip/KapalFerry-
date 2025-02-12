using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CinemachineTouch : MonoBehaviour
{
    public float touchSensitivity = 60f;

    private TouchField touchField;

    // Start is called before the first frame update
    void Start()
    {
        // Set custom input handling for Cinemachine
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;

        // Find the TouchField component in the scene
        touchField = Object.FindFirstObjectByType<TouchField>();

        
    }

    private float HandleAxisInputDelegate(string axisName)
    {
        float value = 0f;

        if (touchField == null)
        {
            return 0f;
        }

        switch (axisName)
        {
            case "Touch X":
                value = touchField.touchDist.x / touchSensitivity;
                break;

            case "Touch Y":
                value = touchField.touchDist.y / touchSensitivity;
                break;

            default:
                
                break;
        }

        
        return value;
    }

}
