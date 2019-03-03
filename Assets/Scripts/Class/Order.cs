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



        //Random Number Of Intensity
        int numberOfIntensity = UnityEngine.Random.Range(numberOfIntensityMin, numberOfIntensityMax);

        List<AlcoholAttribute> attributes = new List<AlcoholAttribute>();
        for (int i = 0; i < numberOfIntensity; ++i)
        {
            int alcoholNumber = Enum.GetValues(typeof(Interactive.Engine.Attribute)).Length;
            Interactive.Engine.Attribute currentAttribute = (Interactive.Engine.Attribute)UnityEngine.Random.Range(0, alcoholNumber);

            bool attributeContained = false;
            int j = 0;
            while(!attributeContained && j < attributes.Count)
            {
                if(currentAttribute == attributes[j].attribute)
                {
                    attributeContained = true;
                    if (attributes[j].intensity != maxIntensity)
                    {
                        attributes[j] = new AlcoholAttribute(currentAttribute, attributes[j].intensity + 1);   
                    }
                    else
                    {
                        numberOfIntensity--;
                    }
                }
                ++j;
            }
            if (attributeContained == false) {
                attributes.Add(new AlcoholAttribute(currentAttribute, 1));
            }

        }


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
