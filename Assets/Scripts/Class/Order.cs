using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;


public class Order : ChemicalElementEntity
{
    static int numberOfIntensityMin = 2;
    static int numberOfIntensityMax = 3;
    static int maxIntensity = 3;

    public static Order GetRandomOrder()
    {
        //random Color
        int colorNumber = Enum.GetValues(typeof(AlcoholColor)).Length;
        int currentColor = UnityEngine.Random.Range(0, colorNumber);




        List<AlcoholAttribute> attributes = new List<AlcoholAttribute>();
        int alcoholNumber = Enum.GetValues(typeof(Interactive.Engine.Attribute)).Length;
        Interactive.Engine.Attribute currentAttribute = (Interactive.Engine.Attribute)UnityEngine.Random.Range(0, alcoholNumber);
        attributes.Add(new AlcoholAttribute(currentAttribute, 0));
 
        return new Order(attributes.ToArray(), (AlcoholColor)currentColor);
    }

    public static void IncrementNumberOfCategoryPoints()
    {
        numberOfIntensityMax++;
    }

    public Order(AlcoholAttribute[] attributes, AlcoholColor color) : base(ChemicalElement.Voidd, attributes, color)
    {

    }
}
