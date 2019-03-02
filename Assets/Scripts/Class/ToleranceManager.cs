using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToleranceManager : Singleton<ToleranceManager>
{
    private float toleranceGaugeMin = 0;
    private float toleranceGaugeMaxNoStress = 33;
    private float toleranceGaugeMaxNormalStress = 66;
    private float toleranceGaugeMax = 100;
    private float toleranceGaugeCurrent = 50;

    public Slider toleranceSliderUI;

    public void UpdateGaugeValue(float value)
    {
        Debug.Log("toleranceGaugeValue before:" + toleranceGaugeCurrent);
        toleranceGaugeCurrent += value;
        if(toleranceGaugeCurrent <= toleranceGaugeMin)
        {
            // YOU LOSE
            Debug.Log("You Lose !");
        }
        else if(toleranceGaugeCurrent >= toleranceGaugeMax)
        {
            // YOU GOOD I DONT EVEN KNOW WHY THIS CONDITION EXIST BUT WELL IT WAS 2A.M AND I WAS EXHAUSTED
        }
        Debug.Log("toleranceGaugeValue after:" + toleranceGaugeCurrent);
    }


}
