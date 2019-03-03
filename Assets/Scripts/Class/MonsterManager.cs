using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
	private Transform folder;

	[Range(0f, 2f)]
	public float maxTimeBetweenMonster = 1.0f;

	private const int MAX_MONSTER_NUMBER = 8;
	public MonsterScript prefabMonster;
	[HideInInspector] public MonsterScript[] monsters;

	public float leaveMinTime = 3f;
	public float leaveMaxTime = 4f;

	//VARIABLE TO MOVE BUT I DONT KNOW WHERE TO PUT IT NOW...
	[HideInInspector] public float orderSuccessValue = 10;

	private float timeToCreateMonster;
	private List<int> freeIndex = new List<int>();
	private float[] xAnchor = new float[] {-6.5f, -4.8f, -2.95f, -1.35f, 0.25f, 2f, 4.25f, 6.5f};


	protected override void Awake()
	{
		base.Awake();

		// initialize random time
		timeToCreateMonster = Time.time + Random.Range(0f, maxTimeBetweenMonster);

		// initialize list
		monsters = new MonsterScript[MAX_MONSTER_NUMBER];
		for(int i = 0; i < MAX_MONSTER_NUMBER; ++i) {
			freeIndex.Add(i);
		}

		// initialize folder
		GameObject fold = new GameObject();
		fold.name = "Monster Folder";
		folder = fold.transform;
	}

	void Update()
	{
		if(freeIndex.Count > 0 && Time.time >= timeToCreateMonster)
		{
			this.CreateMonster();
			timeToCreateMonster += Random.Range(0f, maxTimeBetweenMonster);
		}
	}

	private void CreateMonster()
	{
		// index of free index
		int indexInFreeIndice = UnityEngine.Random.Range(0, freeIndex.Count - 1);

		// pop free index
		int monsterIndex = freeIndex[indexInFreeIndice];
		freeIndex.RemoveAt(indexInFreeIndice);

		// instantiate monster
		MonsterScript obj = Instantiate(prefabMonster);

		// set monster position
		obj.indexInMonsterArray = monsterIndex;
		obj.transform.parent = folder;
		obj.transform.position = new Vector3(
			xAnchor[monsterIndex] + Random.Range(-0.25f, 0.25f),
			Random.Range(-3.25f, -2.75f),
			0f);

		//fill the array of monsters
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
		ToleranceManager.instance.UpdateGaugeValue(-orderSuccessValue);
	}

	public float GetMonsterWaitingDuration()
	{
		return Random.Range(this.leaveMinTime, this.leaveMaxTime);
	}
}
