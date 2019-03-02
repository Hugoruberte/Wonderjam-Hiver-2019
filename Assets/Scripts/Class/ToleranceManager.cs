using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToleranceManager : Singleton<ToleranceManager>
{
    private float toleranceGaugeMin = 0;
    private float toleranceGaugeMax = 100;
    private float toleranceGaugeCurrent = 50;

    void UpdateGaugeValue(float value)
    {
        toleranceGaugeCurrent += value;
        if(toleranceGaugeCurrent <= toleranceGaugeMin)
        {
            // YOU LOSE
        }
        else if(toleranceGaugeCurrent >= toleranceGaugeMax)
        {
            // YOU GOOD I DONT EVEN KNOW WHY THIS CONDITION EXIST
        }
    }


}
