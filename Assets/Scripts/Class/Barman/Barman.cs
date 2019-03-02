using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : Singleton<Barman>
{
    public enum Aspect
    {
        CHILL,
        NORMAL,
        STRESSED,
    };

    private Aspect aspect;
    private GameObject currentCockail;
    private GameObject currentMonster;

    // Start is called before the first frame update
    protected override void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateAspect();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("Le Barman sert un monstre");
            // TODO set cocktail du monstre
            //currentMonster.setCocktail();
        }
    }

    void updateAspect()
    {
        if (StressGauge.instance.currentStress <= StressGauge.instance.maxNoStress)
        {
            aspect = Aspect.CHILL;
        }
        else if (StressGauge.instance.currentStress <= StressGauge.instance.maxNormalStress)
        {
            aspect = Aspect.NORMAL;
        }
        else
        {
            aspect = Aspect.STRESSED;
        }
        //TODO : change sprite renderer 
    }



} 
