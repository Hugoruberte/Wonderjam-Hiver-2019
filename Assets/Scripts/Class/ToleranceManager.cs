using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToleranceManager : Singleton<ToleranceManager>
{
    public float toleranceGaugeMin = 0;
    public float toleranceGaugeMaxNoStress = 33;
    public float toleranceGaugeMaxNormalStress = 66;
    public float toleranceGaugeMax = 100;
    public float toleranceGaugeCurrent = 100;

    public Slider toleranceSliderUI;

    protected override void Start()
    {
        base.Start();

    }

    public void UpdateGaugeValue(float value)
    {
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
        toleranceSliderUI.value = toleranceGaugeCurrent;
        AudioManager.instance.ChangeMixerFromValue(toleranceGaugeCurrent / toleranceGaugeMax);
    }
}
