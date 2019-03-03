using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class RecipeController : Singleton<RecipeController>
{
	private List<ChemicalElement> combo = new List<ChemicalElement>();

	private float length;
	private float height;
	private float widthOfSlot { get { return height - 2 * DISTANCE_BETWEEN_INGREDIENT; }}
	private float left;

	[Header("Prefab")]
	public GameObject slotPrefab;
	private GameObject resultArrowObject;

	[Header("Transform")]
	public Transform mainTransform;
	public Transform resultTransform;
	public Transform recipeSlotsTransform;

	private ShelfController shelfController;
	private BarmanController barmanController;
	private SlotInfoController slotInfoController;

	private ChemicalElementEntity cocktail;

	private const float DISTANCE_BETWEEN_INGREDIENT = 0.12f;
	private const float DISTANCE_BEFORE_RESULT = 1f;

	[SerializeField] private int maxComboLength = 6;

	private SpriteRenderer resultIcon;


	protected override void Awake()
	{
		base.Awake();

		resultIcon = resultTransform.Find("Icon").GetComponent<SpriteRenderer>();
		resultArrowObject = resultTransform.Find("Arrow").gameObject;
	}

	protected override void Start()
	{
		base.Start();

		this.barmanController = BarmanController.instance;
		this.shelfController = ShelfController.instance;
		this.slotInfoController = SlotInfoController.instance;

		this.ClearCocktail();
	}




	public void AddElementToCocktail(ChemicalElement element)
	{
		Debug.Log(element);
		if(combo.Exists(x => x == element) || combo.Count == maxComboLength) {
			return;
		}

		combo.Add(element);

		this.UpdateCocktail();
	}
	private void RemoveElementFromCocktail(OnMouseController click)
	{
		int index = click.transform.GetSiblingIndex();

		CameraEffect.Shake(0.1f);

		this.shelfController.RemovedElementFromCombo(this.combo[index]);

		this.combo.RemoveAt(index);

		this.UpdateCocktail();
	}
	public void ClearCocktail()
	{
		this.combo.Clear();

		this.UpdateCocktail();

		this.shelfController.ClearUsedIngredient();
	}
	public void MakeCocktail()
	{
		if(this.combo.Count == 0) {
			return;
		}

		ChemicalElement element = cocktail.type;

		CameraEffect.Shake(0.5f);

		this.barmanController.HoldCocktail(cocktail);

		this.ClearCocktail();

		this.shelfController.MadeCocktail(element);
		
		this.UpdateResultDisplay(cocktail);
	}



	private void UpdateCocktail()
	{
		ChemicalElementEntity result;

		result = null;
		if(this.combo.Count > 0) {
			result = InteractiveEngine.GetCocktailFrom(this.combo);
		}

		this.cocktail = result;
		
		this.UpdateDisplay(result);
	}
	private void UpdateDisplay(ChemicalElementEntity result)
	{
		this.height = mainTransform.localScale.y - 0.35f;
		this.length = (this.combo.Count > 0) ? ((this.combo.Count + 1) * (DISTANCE_BETWEEN_INGREDIENT + widthOfSlot) +  DISTANCE_BEFORE_RESULT) :(2 * DISTANCE_BETWEEN_INGREDIENT + widthOfSlot);
		this.left = mainTransform.position.x - (this.length / 2f);

		this.mainTransform.localScale = new Vector3(this.length, this.mainTransform.localScale.y, this.mainTransform.localScale.z);

		this.UpdateComboDisplay();
		this.UpdateResultDisplay(result);
	}
	private void UpdateComboDisplay()
	{
		// clean
		for(int i = recipeSlotsTransform.childCount - 1; i >= 0; i--) {
			Destroy(recipeSlotsTransform.GetChild(i).gameObject);
		}

		// create & setup
		GameObject obj;
		Transform tr;
		for(int i = 0; i < this.combo.Count; i++) {
			obj = Instantiate(this.slotPrefab, recipeSlotsTransform.position, Quaternion.identity);
			obj.GetComponentInChildren<OnMouseController>().onClickWithReference.AddListener(RemoveElementFromCocktail);
			obj.GetComponentInChildren<OnMouseController>().onEnterWithReference.AddListener(StartDisplaySlotInfo);
			obj.GetComponentInChildren<OnMouseController>().onExitWithReference.AddListener(EndDisplaySlotInfo);
			tr = obj.transform;

			tr.parent = recipeSlotsTransform;
			tr.position = new Vector3(this.left + i * (DISTANCE_BETWEEN_INGREDIENT + widthOfSlot) + DISTANCE_BETWEEN_INGREDIENT + (widthOfSlot / 2f), tr.position.y, tr.position.z);

			tr.Find("Icon").GetComponent<SpriteRenderer>().sprite = InteractiveEngine.instance.ingredientDatabase.GetIconWith(this.combo[i]);
		}
	}
	private void UpdateResultDisplay(ChemicalElementEntity result)
	{
		if(result != null)
		{
			if(this.combo.Count > 0)
			{
				resultTransform.position = new Vector3(this.left + this.combo.Count * (DISTANCE_BETWEEN_INGREDIENT+widthOfSlot) + DISTANCE_BEFORE_RESULT + (widthOfSlot/2f), resultTransform.position.y, resultTransform.position.z);
				resultArrowObject.SetActive(true);
			}
			else
			{
				resultTransform.position = new Vector3(0f, resultTransform.position.y, resultTransform.position.z);
				resultArrowObject.SetActive(false);
			}

			resultIcon.sprite = InteractiveEngine.instance.ingredientDatabase.GetIconWith(result.type);
		}
		else
		{
			resultTransform.position = new Vector3(0f, resultTransform.position.y, resultTransform.position.z);
			resultArrowObject.SetActive(false);
			resultIcon.sprite = null;
		}
	}


	public void StartDisplaySlotInfo(OnMouseController click)
	{
		int index = click.transform.GetSiblingIndex();

		Ingredient ingredient = InteractiveEngine.instance.ingredientDatabase.ingredients.Find(x => x.element == this.combo[index]);

		this.slotInfoController.Set(click.transform.position, ingredient);
	}

	public void EndDisplaySlotInfo(OnMouseController click)
	{
		this.slotInfoController.Hide();
	}




	public void StartDisplaySlotInfoForResult()
	{
		if(this.cocktail == null) {
			return;
		}

		Ingredient ingredient = InteractiveEngine.instance.ingredientDatabase.ingredients.Find(x => x.element == this.cocktail.type);

		this.slotInfoController.Set(resultTransform.position, ingredient);
	}

	public void EndDisplaySlotInfoForResult()
	{
		this.slotInfoController.Hide();
	}
}
