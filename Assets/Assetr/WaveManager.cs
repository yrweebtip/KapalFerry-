using System;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1.0f;
    public float lenght = 2f;
    public float speed = 1.0f;
    public float offset = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight (float _x)
    {
        return amplitude * Mathf.Sin(_x / lenght + offset);
    }

    internal Vector3 GetWaveNormal(float x)
    {
        throw new NotImplementedException();
    }
}
