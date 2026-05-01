using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] private float maxPowerTime = 30f;
    [SerializeField] private float repairTime = 7f;

    [SerializeField] private float repairDistance = 2.0f; 
    [SerializeField] private GameObject houseLight;
    [SerializeField] private Transform playerPosition;

    private float currentPower;
    private float currentRepairTime;
    private bool isPowerOn = true;
    private bool isRepairing = false;

    void Start()
    {
        currentPower = maxPowerTime;
        SetLights(true);
    }

    void Update()
    {
        if (playerPosition == null) return;

        PowerManagement();
        RepairInteraction();
    }

    void PowerManagement()
    {
        if (isPowerOn)
        {
            currentPower -= Time.deltaTime;

            if (currentPower <= 0)
            {
                currentPower = 0;
                isPowerOn = false;
                SetLights(false);
            }
        }
    }

    void RepairInteraction()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        if (!isPowerOn && distance <= repairDistance)
        {
            if (Input.GetKey(KeyCode.E))
            {
                isRepairing = true;
                currentRepairTime += Time.deltaTime;

                if (currentRepairTime >= repairTime)
                {
                    RepairComplete();
                }
            }
            else
            {
                isRepairing = false;
                currentRepairTime = 0f;
            }
        }
    }

    void RepairComplete()
    {
        isPowerOn = true;
        isRepairing = false;
        currentPower = maxPowerTime;
        currentRepairTime = 0f;
        SetLights(true); 
    }

    void SetLights(bool state)
    {
        if (houseLight != null)
        {
            houseLight.SetActive(state);
        }
    }
}