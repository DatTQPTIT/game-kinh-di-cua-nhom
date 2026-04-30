using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        transform.rotation = cameraTransform.rotation;
    }
}