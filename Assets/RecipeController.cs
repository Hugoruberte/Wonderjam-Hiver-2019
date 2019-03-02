using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class RecipeController : Singleton<RecipeController>
{
	private List<ChemicalElementEntity> combo = new List<ChemicalElementEntity>();

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

	private const float DISTANCE_BETWEEN_INGREDIENT = 0.12f;
	private const float DISTANCE_BEFORE_RESULT = 1f;

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

		// combo.Add(new BloodySlimy());
		// combo.Add(new DraculaSunrise());
		// combo.Add(new StickyTear());

		// AddElementToCocktail(new BigIsland());

		this.ClearCocktail();
	}




	public void AddElementToCocktail(ChemicalElementEntity element)
	{
		if(combo.Exists(x => x.type == element.type)) {
			Debug.LogWarning($"WARNING : Element {element} already in combo !");
			return;
		}

		combo.Add(element);

		this.UpdateCocktail();
	}
	public void RemoveElementFromCocktail(ChemicalElementEntity element)
	{
		this.combo.Remove(element);

		this.UpdateCocktail();
	}
	public void ClearCocktail()
	{
		this.combo.Clear();

		this.UpdateCocktail();
	}



	private void UpdateCocktail()
	{
		ChemicalElementEntity result;

		result = null;
		if(this.combo.Count > 0) {
			if(this.combo.Count < 3) {
				result = this.combo[0];
				for(int i = 1; i < this.combo.Count; i++) {
					result *= this.combo[i];
				}
			} else {
				result = InteractiveEngine.GetCocktailFrom(this.combo);
			}
		}
		
		this.UpdateDisplay(result);
	}

	

	private void UpdateDisplay(ChemicalElementEntity result)
	{
		this.height = mainTransform.localScale.y;
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
			tr = obj.transform;

			tr.parent = recipeSlotsTransform;
			tr.position = new Vector3(this.left + i * (DISTANCE_BETWEEN_INGREDIENT + widthOfSlot) + DISTANCE_BETWEEN_INGREDIENT + (widthOfSlot / 2f), tr.position.y, tr.position.z);

			tr.Find("Icon").GetComponent<SpriteRenderer>().sprite = InteractiveEngine.instance.elementIconDatabase.GetIconWith(this.combo[i].type);
		}
	}
	private void UpdateResultDisplay(ChemicalElementEntity result)
	{
		if(result != null) {
			if(this.combo.Count > 0) {
				resultTransform.position = new Vector3(this.left + this.combo.Count * (DISTANCE_BETWEEN_INGREDIENT+widthOfSlot) + DISTANCE_BEFORE_RESULT + (widthOfSlot/2f), resultTransform.position.y, resultTransform.position.z);
			} else {
				resultTransform.position = new Vector3(0f, resultTransform.position.y, resultTransform.position.z);
				resultArrowObject.SetActive(false);
			}

			resultIcon.sprite = InteractiveEngine.instance.elementIconDatabase.GetIconWith(result.type);

		} else {
			// anim result go away
		}
	}
}
