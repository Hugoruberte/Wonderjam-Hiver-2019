using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class MonsterScript : MonoBehaviour
{
	private Transform myTransform;

	public int monsterAspect;
	public float smooth = 0.25f;

	[HideInInspector] public int indexInMonsterArray;
	private float startWaitingTime;
	private float willBeWaiting;
	private float timeCoeff = 5;
	private float timeLimit = 0.25f;
	private float colorCoeff = 2;
	private float categoryCoeff = 2;

	private bool iHaveNotBeenServed = true;

	private WaitForSeconds waitBeforeLeaving;

	private IEnumerator waitForCocktailCoroutine = null;

	private Order myOrder;

	private MonsterManager monsterManager;


	void Awake()
	{
		myTransform = transform;
	}

	void Start()
	{
		monsterManager = MonsterManager.instance;

		myOrder = Order.GetRandomOrder();

		startWaitingTime = Time.time;
		willBeWaiting = monsterManager.GetMonsterWaitingDuration();
		waitBeforeLeaving = new WaitForSeconds(willBeWaiting);

		waitForCocktailCoroutine = WaitForCocktailCoroutine();
		StartCoroutine(waitForCocktailCoroutine);
	}

    private void Update()
    {
        transform.LookAt(BarmanController.instance.transform);
    }

    private IEnumerator WaitForCocktailCoroutine()
	{
		Vector3 target;
		Vector3 reference = Vector3.zero;

		// Walk in
		target = myTransform.position + Vector3.up + Vector3.forward;
		while(Vector3.Distance(myTransform.position, target) > 0.05f) {
			myTransform.position = Vector3.SmoothDamp(myTransform.position, target, ref reference, smooth);
			yield return null;
		}

		// Ask + spawn Clock
		BubbleManager.instance.SpawnBubble(this, myTransform.position, myOrder);
	}

	public void OnEndSpawnBubble()
	{
		// only does that
		ClockManager.instance.SpawnClock(this, myTransform.position, willBeWaiting);

		StopCoroutine(waitForCocktailCoroutine);
		waitForCocktailCoroutine = ContinueWaitForCocktailCoroutine();
		StartCoroutine(waitForCocktailCoroutine);
	}

	private IEnumerator ContinueWaitForCocktailCoroutine()
	{
		// Wait
		yield return waitBeforeLeaving;

		yield return this.LeaveCoroutine();
	}

	private IEnumerator LeaveCoroutine()
	{
		Vector3 target;
		Vector3 reference = Vector3.zero;

        Debug.Log("Leave" + name);
		// Leaving
		monsterManager.MonsterStartLeaving(this.indexInMonsterArray);

		// Walk out
		target = myTransform.position - Vector3.up * 2f - Vector3.forward * 2f;
		while(Vector3.Distance(myTransform.position, target) > 0.05f) {
			myTransform.position = Vector3.SmoothDamp(myTransform.position, target, ref reference, smooth);
			yield return null;
		}

		if(iHaveNotBeenServed) {
			monsterManager.UpdateGaugeOnTimeEnd(this.indexInMonsterArray);
		}
		Destroy(gameObject);
	}

	public void ReceiveCocktail(ChemicalElementEntity Cocktail)
	{
		// Appelez la fonction de Luc de satisfaction et renvoyez au MonsterManager la valeur a ajouter à la satisfaction globale
		float tolerancePoint = CalculateTolerancePoint(myOrder, Cocktail, false);

		this.iHaveNotBeenServed = false;

		ToleranceManager.instance.UpdateGaugeValue(tolerancePoint);

		StopCoroutine(waitForCocktailCoroutine);
		StartCoroutine(this.LeaveCoroutine());
	}









	//calculateTolerancePoint
	private float CalculateTolerancePoint(ChemicalElementEntity Order, ChemicalElementEntity Cocktail, bool orderIsACocktail)
	{
		//time points
		float toleranceTimePoints = CalculateTimeTolerancePoint();

		//color Points
		float toleranceColorPoints = CalculateColorTolerancePoints(myOrder.colors, Cocktail.colors);

		//category Points
		float toleranceCategoryPoints = CalculateCategoryTolerancePoint(myOrder.attributes, Cocktail.attributes, orderIsACocktail);

		return toleranceTimePoints + toleranceColorPoints + toleranceCategoryPoints;

	}
	
	//time points
	private float CalculateTimeTolerancePoint()
	{
		float currentWaitingTime = Time.time;
		//actual waiting Time
		float waitingTime = currentWaitingTime - startWaitingTime;

		//tolerance points lost from time
		return ((waitingTime / willBeWaiting) - timeLimit) * timeCoeff;
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
		
		float toleranceColorPoints = -colorCoeff;
		bool colorNotFound = true;
		int i = 0;
		while (colorNotFound && i < colors.Length)
		{
			if (colorWanted == colors[i])
			{
				toleranceColorPoints *= -1;
				colorNotFound = false;
			}
			++i;
		}
		return toleranceColorPoints;
	}

	//category points
	private float CalculateCategoryTolerancePoint(AlcoholAttribute[] orderAttributes, AlcoholAttribute[] cocktailAttributes, bool orderIsACocktail)
	{
		//create Map Order Attributes
		Dictionary<Attribute, float> IntensityForAttribute = new Dictionary<Attribute, float>();

		for (int i = 0; i < orderAttributes.Length; ++i)
		{
			AlcoholAttribute tmpAttribute = orderAttributes[i];
			IntensityForAttribute.Add(tmpAttribute.attribute, tmpAttribute.intensity);
		}
        float toleranceCategoryPositivePoints = 0;
        float toleranceCategoryNegativePoints = 0;
        if (cocktailAttributes != null)
        {
            
            //for all cocktailAttributes
            for (int i = 0; i < cocktailAttributes.Length; ++i)
            {
                AlcoholAttribute tmpAttribute = cocktailAttributes[i];
                //if the attributes is also in the order
                if (IntensityForAttribute.ContainsKey(tmpAttribute.attribute))
                {
                    toleranceCategoryPositivePoints += Mathf.Min(IntensityForAttribute[tmpAttribute.attribute], tmpAttribute.intensity);
                    //the tolerance point is the difference between the wantedValue and the givenValue
                    IntensityForAttribute[tmpAttribute.attribute] -= tmpAttribute.intensity;
                }
                else if(orderIsACocktail)
                {
                    //else the category was not wanted, all points in this category are false.
                    toleranceCategoryNegativePoints += tmpAttribute.intensity;
                }
            }
        }

        
		foreach (Attribute attribute in IntensityForAttribute.Keys)
		{
            toleranceCategoryNegativePoints += Mathf.Abs(IntensityForAttribute[attribute]);
		}
        toleranceCategoryNegativePoints *= categoryCoeff;


		return toleranceCategoryPositivePoints - toleranceCategoryNegativePoints;
	}
}
