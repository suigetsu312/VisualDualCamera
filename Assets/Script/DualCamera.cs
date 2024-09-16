using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DualCamera : MonoBehaviour
{

    [SerializeField]
    public Camera LeftCamera;

    [SerializeField]
    public Camera RightCamera;

    [Inject]
    GameObjectTools _gameObjectTools { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        LeftCamera = _gameObjectTools.FindChildrenByName<Camera>(this, "LeftCamera");
        RightCamera = _gameObjectTools.FindChildrenByName<Camera>(this, "RightCamera");

        if (LeftCamera != null)
        {
            Debug.Log("Left Camera focal length: " + LeftCamera.focalLength);
        }
        else
        {
            Debug.LogError("Left Camera not found!");
        }

        if (RightCamera != null)
        {
            Debug.Log("Right Camera focal length: " + RightCamera.focalLength);
        }
        else
        {
            Debug.LogError("Right Camera not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftCamera != null)
        {
            Debug.Log("Left Camera focal length: " + LeftCamera.focalLength);
        }

    }
}
 