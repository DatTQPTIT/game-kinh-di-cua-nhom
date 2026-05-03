using UnityEngine;

public class FlashlightDetector : MonoBehaviour
{
    [SerializeField] private GameObject flashlightLight;

    private void OnTriggerStay(Collider other)
    {
        if(flashlightLight != null && flashlightLight.activeSelf)
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
