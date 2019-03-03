﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;


public class Unicorn : MonsterScript
{
    public float stayingTime;
    public float minSpawnInterval, maxSpawnInterval;
    public float gaugeBonus;

    private Animator myAnim;

    [HideInInspector] public bool isHere = false;

    private float spawnTime;
    private ChemicalElement cocktail;
    private Order myOrder;

    // Start is called before the first frame update
    protected override void Start()
    {
    	myAnim = GetComponent<Animator>();

        cocktail = ChemicalElement.BigIsland;
        _ChemicalElementEntity ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(cocktail);
        myOrder = new Order(ent.attributes, ent.colors[0]);
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            spawnTime += Random.Range(minSpawnInterval, maxSpawnInterval);
            getIn();
        }
    }
    
    private void getIn()
    {
        Debug.Log("is here");
        isHere = true;
    }
   
    public void receiveCocktail(ChemicalElementEntity Cocktail)
    {
        Debug.Log("receive Cocktqil");
        if (Cocktail.type == cocktail)
        {
            ToleranceManager.instance.UpdateGaugeValue(gaugeBonus);
        }
        else
        {
            ToleranceManager.instance.UpdateGaugeValue(gaugeBonus/2);
        }

        getOut();
    }

    private void getOut()
    {
        Debug.Log("Get out");
        isHere = false;
    }
}
