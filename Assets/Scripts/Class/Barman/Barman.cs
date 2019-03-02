using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class Barman : Singleton<Barman>
{
	public enum Aspect
	{
		CHILL,
		NORMAL,
		STRESSED,
		TERRIFIED
	};

	[Range(0.01f, 1f)]
	public float smooth = 0.2f;

	private Transform myTransform;

	private Aspect aspect;
	private MonsterScript currentMonster; 

	private MonsterScript[] monsters;

	private int vertCache = 0;

	private bool isMoving = false;

	private IEnumerator moveCoroutine = null;

	private Vector3 myReference = Vector3.zero;

	protected override void Awake()
	{
		base.Awake();

		myTransform = transform;
	}

	protected override void Start()
	{
		base.Start();

		monsters = MonsterManager.instance.monsters;

		StartCoroutine(MovementCoroutine());
	}

	private IEnumerator MovementCoroutine()
	{
		int vert, down;
		MonsterScript monster;

		while(true) {

			do {
				vert = (int)Input.GetAxisRaw("Horizontal");
				down = (int)Input.GetAxisRaw("Vertical");

				yield return null;
			}
			while(vert == 0 && down >= 0);

			monster = null;

			if(vert < 0) {
				for(int i = this.monsters.Length - 1; i >= 0; i--) {
					if(this.monsters[i] != null && this.monsters[i].transform.position.x < myTransform.position.x) {
						monster = this.monsters[i];
					}
				}
			} else if(vert > 0) {
				for(int i = 0; i < this.monsters.Length; i++) {
					if(this.monsters[i] != null && this.monsters[i].transform.position.x > myTransform.position.x) {
						monster = this.monsters[i];
					}
				}
			} else {
				Debug.Log("Le Barman sert un monstre");
			}

			if(monster != null && currentMonster != monster && (!isMoving || vert != vertCache)) {
				this.vertCache = vert;
				this.currentMonster = monster;

				if(moveCoroutine != null) {
					StopCoroutine(moveCoroutine);
				}
				moveCoroutine = MoveCoroutine(currentMonster);
				StartCoroutine(moveCoroutine);
			}
		}
	}

	private IEnumerator MoveCoroutine(MonsterScript currentMonster)
	{
		this.isMoving = true;
		Vector3 target = new Vector3(currentMonster.transform.position.x, myTransform.position.y, myTransform.position.z);

		while(Vector3.Distance(myTransform.position, target) > 0.1f)
		{
			myTransform.position = Vector3.SmoothDamp(myTransform.position, target, ref myReference, smooth);

			yield return null;
		}

		this.isMoving = false;
	}

	private void UpdateAspect()
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
