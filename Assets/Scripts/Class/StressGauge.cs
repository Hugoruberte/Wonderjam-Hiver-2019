using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressGauge : Singleton<StressGauge>
{
    public int maxStress = 100;
    public int maxNormalStress = 66;
    public int maxNoStress = 33;

    public Slider stressSliderUI;

    public int currentStress = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStress >= 100)
        {
            barmanDeath();
        }

        //update UI
        //stressSliderUI.value = currentStress;
    }

    void barmanDeath()
    {
        Debug.Log("BARMAN DEAD RIP");
        //TODO : game over UI
    }
}
