using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;
    public KeyCode key = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}