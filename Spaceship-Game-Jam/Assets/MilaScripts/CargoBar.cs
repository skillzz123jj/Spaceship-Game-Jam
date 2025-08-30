using UnityEngine;
using UnityEngine.UI;

public class CargoBar : MonoBehaviour
{
    public Image fillImage;     
    public int maxValue = 100;   
    private int currentValue = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddValue(5);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveValue();
        }


    }
    void Start()
    {
        UpdateBar();
    }


    public void AddValue(int amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue); 
        UpdateBar();
    }

    public void RemoveValue()
    {
        currentValue = 0;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        UpdateBar();
    }

    void UpdateBar()
    {
        float fillPercent = (float)currentValue / maxValue;
        fillImage.fillAmount = fillPercent;
    }
}
