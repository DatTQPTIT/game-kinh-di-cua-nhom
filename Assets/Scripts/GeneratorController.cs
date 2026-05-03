using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] private float maxPowerTime = 30f;
    [SerializeField] private float repairTime = 7f;

    [SerializeField] private float repairDistance = 2.0f; 
    [SerializeField] private GameObject houseLight;
    [SerializeField] private Transform playerPosition;


    [SerializeField] private GameObject repairUI;
    [SerializeField] private Image repairProgressBar;
    [SerializeField] private GameObject repairText;

    private float currentPower;
    private float currentRepairTime;
    public static bool isPowerOn = true;
    private bool isRepairing = false;

    void Start()
    {
        currentPower = maxPowerTime;
        isPowerOn = true;
        SetLights(true);

        if(repairUI != null ) repairUI.SetActive(false);
        if(repairText != null) repairText.SetActive(false);
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
        if (isPowerOn)
        {
            if (repairUI != null) repairUI.SetActive(false);
            if (repairText != null) repairText.SetActive(false);
            currentRepairTime = 0f;

            return;
        }

        float distance = Vector3.Distance(transform.position, playerPosition.position);

        if (distance <= repairDistance)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (repairUI != null) repairUI.SetActive(true);
                if (repairText != null) repairText.SetActive(false);

                currentRepairTime += Time.deltaTime;

                if (repairProgressBar != null)
                {
                    repairProgressBar.fillAmount = currentRepairTime / repairTime;
                }

                if (currentRepairTime >= repairTime)
                {
                    RepairComplete();
                }
            }
            else
            {
                if (repairUI != null) repairUI.SetActive(false);
                if (repairText != null) repairText.SetActive(true);

                currentRepairTime = 0f;
                if (repairProgressBar != null) repairProgressBar.fillAmount = 0f;
            }
        }
        else
        {
            if (repairUI != null) repairUI.SetActive(false);
            if (repairText != null) repairText.SetActive(false);
            currentRepairTime = 0f;
        }
    }

    void RepairComplete()
    {
        isPowerOn = true;
        isRepairing = false;
        currentPower = maxPowerTime;
        currentRepairTime = 0f;
        SetLights(true);

        if (repairUI != null) repairUI.SetActive(false);
        if (repairText != null) repairText.SetActive(false);
    }

    void SetLights(bool state)
    {
        if (houseLight != null)
        {
            houseLight.SetActive(state);
        }
    }
}