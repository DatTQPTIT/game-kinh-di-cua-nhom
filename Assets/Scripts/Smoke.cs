using UnityEngine;

public class FogDrift : MonoBehaviour
{
    public float driftSpeed = 0.2f;

    void Update()
    {
        transform.position += new Vector3(
            Mathf.Sin(Time.time * 0.3f),
            0,
            Mathf.Cos(Time.time * 0.2f)
        ) * driftSpeed * Time.deltaTime;
    }
}