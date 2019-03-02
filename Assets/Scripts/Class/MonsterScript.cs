using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class MonsterScript : MonoBehaviour
{
    //monster variables
    public int position;
    public int index;
    public int monsterAspect;

    //time variables
    public float waiting = 2.0f;

    private WaitForSeconds waitBeforeLeaving;
    private float startWaitingTime;
    private float timeCoeff = 10;
    private float timeLimit = 0.5f;

    //order Variable
    private Order myOrder;
    private Ambrosia cocktailTest;

    //color variable
    private float colorCoeff = 7;

    //category variable
    private float categoryCoeff = 2;

    // Start is called before the first frame update
    void Start()
    {
        startWaitingTime = Time.time;
        waitBeforeLeaving = new WaitForSeconds(waiting);
        StartCoroutine(Leaving());

        /*TODO Random Attribution of order just test value here */
        AlcoholColor color = AlcoholColor.Black;
        AlcoholAttribute[] attributesList = new AlcoholAttribute[2];

        attributesList[0] = (new AlcoholAttribute(Attribute.Fatal, 2));
        attributesList[1] = (new AlcoholAttribute(Attribute.Spicy, 3));

        cocktailTest = new Ambrosia();


        
        myOrder = new Order(attributesList, color);
        /* END OF TEST HERE */



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Leaving()
    {
        yield return waitBeforeLeaving;
        MonsterManager.instance.TimerEnd(index);
    }
    void OnWaitingEnd()
    {
        MonsterManager.instance.TimerEnd(position);
    }

    void SetCocktail(ChemicalElementEntity Order, ChemicalElementEntity Cocktail)
    {
        // Appelez la fonction de Luc de satisfaction et renvoyez au MonsterManager la valeur a ajouter à la satisfaction globale
        float tolerancePoint = CalculateTolerancePoint(Order, Cocktail);

        ToleranceManager.instance.UpdateGaugeValue(tolerancePoint);

        MonsterManager.instance.LeavingMonster(index);
    }

    //calculateTolerancePoint
    private float CalculateTolerancePoint(ChemicalElementEntity Order, ChemicalElementEntity Cocktail)
    {
        //time points
        float toleranceTimePoints = CalculateTimeTolerancePoint();

        //color Points
        float toleranceColorPoints = CalculateColorTolerancePoints(myOrder.colors, Cocktail.colors);

        //category Points
        float toleranceCategoryPoints = CalculateCategoryTolerancePoint(myOrder.attributes, Cocktail.attributes);

        return MonsterManager.instance.orderSuccessValue - toleranceTimePoints - toleranceColorPoints - toleranceCategoryPoints;

    }
    
    //time points
    private float CalculateTimeTolerancePoint()
    {
        float currentWaitingTime = Time.time;
        //actual waiting Time
        float waitingTime = currentWaitingTime - startWaitingTime;

        //tolerance points lost from time
        return ((waitingTime / waiting) - timeLimit) * timeCoeff;
    }

    //calculate errors due to colors
    private float CalculateColorTolerancePoints(AlcoholColor []colorsWanted, AlcoholColor [] colors)
    {
        float toleranceColorPoints = 0;
        foreach(AlcoholColor color in colorsWanted)
        {
            toleranceColorPoints += CalculateColorTolerancePoint(color, colors);
        }
        return toleranceColorPoints;
    }

    //calculate error of one color
    private float CalculateColorTolerancePoint(AlcoholColor colorWanted,AlcoholColor []colors)
    {
        
        float toleranceColorPoints = colorCoeff;
        bool colorNotFound = true;
        int i = 0;
        while (colorNotFound && i < colors.Length)
        {
            if (colorWanted == colors[i])
            {
                toleranceColorPoints *= 0;
                colorNotFound = false;
            }
            ++i;
        }
        return toleranceColorPoints;
    }

    //category points
    private float CalculateCategoryTolerancePoint(AlcoholAttribute [] orderAttributes, AlcoholAttribute []cocktailAttributes)
    {
        //create Map Order Attributes
        Dictionary<Attribute, float> IntensityForAttribute = new Dictionary<Attribute, float>();

        for (int i = 0; i < orderAttributes.Length; ++i)
        {
            AlcoholAttribute tmpAttribute = orderAttributes[i];
            IntensityForAttribute.Add(tmpAttribute.attribute, tmpAttribute.intensity);
        }


        //for all cocktailAttributes
        for (int i = 0; i < cocktailAttributes.Length; ++i)
        {
            AlcoholAttribute tmpAttribute = cocktailAttributes[i];
            //if the attributes is also in the order
            if (IntensityForAttribute.ContainsKey(tmpAttribute.attribute))
            {
                //the tolerance point is the difference between the wantedValue and the givenValue
                IntensityForAttribute[tmpAttribute.attribute] -= tmpAttribute.intensity;
            }
            else
            {
                //else the category was not wanted, all points in this category are false.
                IntensityForAttribute.Add(tmpAttribute.attribute, tmpAttribute.intensity);
            }
        }


        //calculate toleranceValue
        float toleranceCategoryPoints = 0;

        foreach (Attribute attribute in IntensityForAttribute.Keys)
        {
            toleranceCategoryPoints += Mathf.Abs(IntensityForAttribute[attribute]);
        }

        toleranceCategoryPoints *= categoryCoeff;

        return toleranceCategoryPoints;
    }



}
