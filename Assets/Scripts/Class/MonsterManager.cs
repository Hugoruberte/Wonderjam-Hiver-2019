using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
	private Transform folder;

	[Range(0f, 25f)]
	public float timeBetweenMonster = 25.0f;
    public float coefBetweenMonster = 1.04f;
    [Range(0f, 120f)]
    public float timeBetweenIncreaseCategory = 120f;

    public float leaveTime = 30f;

    public float OverTimePoints = 10;

    private const int MAX_MONSTER_NUMBER = 7;
	[HideInInspector] public MonsterScript[] monsters;

	public MonsterScript[] monstersPrefab;

	public MonsterScript _tmp_prefabMonster;

    private float timeToIncreaseCategory;
    private float timeToCreateMonster;
	private List<int> freeIndex = new List<int>();
	private float[] xAnchor = new float[MAX_MONSTER_NUMBER];
    private float xMin = -2.75f;


	protected override void Awake()
	{
		base.Awake();

        // initialize random time
        timeToCreateMonster = Time.time;
        timeToIncreaseCategory = Time.time + timeBetweenIncreaseCategory;

		// initialize list
		monsters = new MonsterScript[MAX_MONSTER_NUMBER];
		for(int i = 0; i < MAX_MONSTER_NUMBER; ++i) {
			freeIndex.Add(i);
		}

		// initialize folder
		GameObject fold = new GameObject();
		fold.name = "Monster Folder";
		folder = fold.transform;


        float spaceBetweenX = ((xMin * -2) + 1) / xAnchor.Length;
        for(int i = 0; i < xAnchor.Length; ++i)
        {
            xAnchor[i] = xMin + spaceBetweenX * i;
        }
	}

	void Update()
	{
		if(freeIndex.Count > 0 && Time.time >= timeToCreateMonster)
		{
            this.CreateMonster();
			timeToCreateMonster = Time.time + timeBetweenMonster;
            timeBetweenMonster /= coefBetweenMonster;
		}

        if( Time.time >= timeToIncreaseCategory)
        {
            Order.IncrementNumberOfCategoryPoints();
            timeToIncreaseCategory = Time.time + timeBetweenIncreaseCategory;
        }
	}

	private void CreateMonster()
	{
		// index of free index
		int indexInFreeIndice = UnityEngine.Random.Range(0, freeIndex.Count - 1);

		// pop free index
		int monsterIndex = freeIndex[indexInFreeIndice];
		freeIndex.RemoveAt(indexInFreeIndice);

        int monsterArchetypeRandom = Random.Range(0, monstersPrefab.Length);
		// instantiate monster
		MonsterScript obj = Instantiate(monstersPrefab[monsterArchetypeRandom]);

		// set monster position
		obj.indexInMonsterArray = monsterIndex;
		obj.transform.parent = folder;
		obj.transform.position = new Vector3(
	    xAnchor[monsterIndex] + Random.Range(-0.25f, 0.25f),
		Random.Range(-2.25f, -2f),
		 	-7f);

		// //fill the array of monsters    
		this.monsters[monsterIndex] = obj;
	}

	public void MonsterStartLeaving(int index)
	{
		this.monsters[index] = null;
		freeIndex.Add(index);
	}

	public void UpdateGaugeOnTimeEnd(int index)
	{
		// only does that
		ToleranceManager.instance.UpdateGaugeValue(-OverTimePoints);
	}

	public float GetMonsterWaitingDuration()
	{
        return leaveTime;
	}
}
