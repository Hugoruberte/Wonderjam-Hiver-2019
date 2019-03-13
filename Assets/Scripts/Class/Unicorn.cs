using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;
using Tools;

public class Unicorn : MonsterScript
{
	public float stayingTime;
	public float minSpawnInterval, maxSpawnInterval;
	public float gaugeBonus;
	public float bubbleSeconds;

	private Animator myAnim;

	[HideInInspector] public bool isHere = false;

	private float spawnTime;
	private ChemicalElement cocktail;
	private Order myOrder;
	private WaitForSeconds bubbleTime;

	protected override void Start()
	{
		myAnim = GetComponent<Animator>();

		cocktail = ChemicalElement.BigIsland;
		_ChemicalElementEntity ent = InteractiveEngine.instance.interactiveEngineData.GetChemicalElementDataWithEnum(cocktail);
		myOrder = new Order(ent.attributes, ent.colors[0]);
		spawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
		bubbleTime = new WaitForSeconds(bubbleSeconds);
	}

	void Update()
	{
		if(Time.time > spawnTime && !isHere)
		{
			spawnTime += Random.Range(minSpawnInterval, maxSpawnInterval);
			GetIn();
		}
	}
	
	private void GetIn()
	{
		myAnim.SetBool("Show", true);
		isHere = true;
		StartCoroutine(SpawnBubble());
	}
   
	private IEnumerator SpawnBubble()
	{
		Transform bubble = TransformExtension.DeepFind(transform, "Bubble");
		bubble.gameObject.SetActive(true);
		yield return bubbleTime;
		bubble.gameObject.SetActive(false);
	}


	public virtual void ReceiveCocktail(ChemicalElementEntity Cocktail)
	{
		if (Cocktail.type == cocktail)
		{
			RainbowManager.instance.SpawnRainbow();
			ToleranceManager.instance.UpdateGaugeValue(gaugeBonus);
		}
		else
		{
			ToleranceManager.instance.UpdateGaugeValue(gaugeBonus/2);
		}

		GetOut();
	}

	private void GetOut()
	{
		myAnim.SetBool("Show", false);
		isHere = false;
	}
}

