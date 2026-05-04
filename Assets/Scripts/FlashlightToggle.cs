using UnityEngine;
using UnityEngine.UI;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;
    public KeyCode key = KeyCode.F;

    public float maxBattery = 100f;   
    public float currentBattery;       
    public float drainRate = 5f;

    public Slider batterySlider;

    void Start()
    {
        currentBattery = maxBattery;

        if (flashlight != null)
        {
            flashlight.enabled = false; 
        }

        if (batterySlider != null)
        {
            batterySlider.maxValue = maxBattery;
            batterySlider.value = currentBattery;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (currentBattery > 0 || flashlight.enabled)
            {
                flashlight.enabled = !flashlight.enabled;
            }
        }
        if (flashlight.enabled)
        {
            if (currentBattery > 0)
            {
                //Bat va tru pin theo giay
                currentBattery -= drainRate * Time.deltaTime;
                UpdateUI();
            }
            else
            {
                //Het pin tu dong tat
                flashlight.enabled = false;
                currentBattery = 0;
                UpdateUI();
            }
        }
    }
    void UpdateUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery;
        }
    }

    //nhat pin de bo sung pin den?
    public void Recharge(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0, maxBattery);
        UpdateUI();
    }
}