using UnityEngine;

public class FlashlightDetector : MonoBehaviour
{
    [SerializeField] private GameObject flashlightLight;
    private Light lightComp;

    void Start()
    {
        if(flashlightLight != null)
        {
            lightComp = flashlightLight.GetComponent<Light>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool isLampOn = flashlightLight != null && flashlightLight.activeInHierarchy && (lightComp == null || lightComp .enabled);

        if(isLampOn)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.GetHitByFlashlight();
                }
            }
        }
    }
}
