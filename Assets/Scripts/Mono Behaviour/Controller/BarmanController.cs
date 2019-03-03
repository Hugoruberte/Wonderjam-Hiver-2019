using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class BarmanController : Singleton<BarmanController>
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

	public SpriteRenderer tray;

    public Unicorn unicorn;

	private Transform myTransform;

	private Aspect aspect;

	private MonsterScript currentMonster; 
	private MonsterScript[] monsters;

	private bool isMoving = false;

	private IEnumerator moveCoroutine = null;

	private Vector3 myReference = Vector3.zero;

	private ToleranceManager toleranceGauge;

	private WaitForSeconds waitAfterService = new WaitForSeconds(0.1f);

	private ChemicalElementEntity currentCocktail = null;

    


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

		toleranceGauge = ToleranceManager.instance;
	}

	public void HoldCocktail(ChemicalElementEntity cocktail)
	{
		this.currentCocktail = cocktail;

		tray.sprite = InteractiveEngine.instance.ingredientDatabase.GetIconWith(currentCocktail.type);
	}

	public void ReleaseCocktail()
	{
		CameraEffect.Shake(0.2f);

		tray.sprite = null;
	}

	private void CocktailServiceToCurrentMonster()
	{
		currentMonster.ReceiveCocktail(this.currentCocktail);

		this.currentCocktail = null;
		tray.sprite = null;
	}

	private IEnumerator MovementCoroutine()
	{
		int vert, down;
		float threshold;
		MonsterScript monster;

		while(true) {

			do {
				vert = (int)Input.GetAxisRaw("Horizontal");
				down = (int)Input.GetAxisRaw("Vertical");

				yield return null;
			}
			while(vert == 0 && down >= 0);

			monster = null;

			if(vert < 0)
			{
				threshold = float.MinValue;
				for(int i = 0; i < this.monsters.Length; i++) {
					if(this.monsters[i] != null
						&& this.monsters[i].transform.position.x < myTransform.position.x - 0.1f
						&& this.monsters[i].transform.position.x > threshold)
					{
						threshold = this.monsters[i].transform.position.x;
						monster = this.monsters[i];
					}
				}
			}
			else if(vert > 0)
			{
				threshold = float.MaxValue;
				for(int i = 0; i < this.monsters.Length; i++) {
					if(this.monsters[i] != null
						&& this.monsters[i].transform.position.x > myTransform.position.x + 0.1f
						&& this.monsters[i].transform.position.x < threshold)
					{
						threshold = this.monsters[i].transform.position.x;
						monster = this.monsters[i];
					}
                    else if (unicorn.isHere && currentCocktail != null)
                    {
                        monster = unicorn;
                        unicorn.receiveCocktail(currentCocktail);
                    }
				}
			}
			else if(currentCocktail != null && currentMonster != null)
			{
				CameraEffect.Shake(0.2f);

				this.CocktailServiceToCurrentMonster();

				yield return waitAfterService;
			}

			if(monster != null && currentMonster != monster && !isMoving) {
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
		if (toleranceGauge.toleranceGaugeCurrent <= toleranceGauge.toleranceGaugeMaxNoStress)
		{
			aspect = Aspect.CHILL;
		}
		else if (toleranceGauge.toleranceGaugeCurrent <= toleranceGauge.toleranceGaugeMaxNormalStress)
		{
			aspect = Aspect.NORMAL;
		}
		else if (toleranceGauge.toleranceGaugeCurrent < toleranceGauge.toleranceGaugeMax)
		{
			aspect = Aspect.STRESSED;
		}
		else {
			aspect = Aspect.TERRIFIED;
		}
		//TODO : change sprite renderer 
	}
} 
