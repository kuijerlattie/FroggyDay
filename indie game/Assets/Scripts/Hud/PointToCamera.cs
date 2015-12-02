using UnityEngine;
using System.Collections;

/// <summary>
/// Sets the rotation and scale of a canvas to match the camera's view
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PointToCamera : MonoBehaviour {

    Camera _camera;
    RectTransform _rect;

    // Use this for initialization
    void Start () {
        _camera = Camera.main;
        _rect = GetComponent<RectTransform>();
    }

    public void UpdateRotation(Quaternion rotation)
    {
        _rect.rotation = rotation;
    }
}
