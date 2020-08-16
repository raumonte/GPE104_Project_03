using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour
{
    public float currentValue; //This is the current value of the percentage.
    public float maxValue;  //This is the max value of the percentage.
    private Image imageComponent; // This is where the Image Component can be called.
    public Color barColor = Color.green; //
    public Color barCriticalColor = Color.cyan;
    public float criticalLevel = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the objects component image. 
        imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            currentValue = maxValue;
            //This can give us the percentage of the image shown.
            imageComponent.type = Image.Type.Filled;
            //This sets the fill method to horizontal.
            imageComponent.fillMethod = Image.FillMethod.Horizontal;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float percentOfMax = currentValue / maxValue;
        imageComponent.fillAmount = percentOfMax;
        if (percentOfMax > criticalLevel)
        {
            imageComponent.color = barColor;
        }
        else
        {
            imageComponent.color = barCriticalColor;
        }
    }
}
