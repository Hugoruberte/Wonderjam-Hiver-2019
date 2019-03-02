using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class ShelfController : Singleton<ShelfController>
{
	[Header("Prefab")]
	public GameObject slotPrefab;

	[Header("Transform")]
	public Transform mainTransform;
	public Transform firstSlotsTransform;
	public Transform secondSlotsTransform;
	public Transform leftAnchor;

	private Transform currentShelf;

	private const int NUMBER_INGREDIENT_BY_ROW = 8;
	private const int NUMBER_INGREDIENT_BY_COLUMN = 2;
	private const int NUMBER_INGREDIENT_BY_SHELF = NUMBER_INGREDIENT_BY_ROW * NUMBER_INGREDIENT_BY_COLUMN;

	private int shelfIndex = 0;

	private IngredientDatabase ingredientDatabase;

	protected override void Start()
	{
		this.ingredientDatabase = InteractiveEngine.instance.ingredientDatabase;

		currentShelf = firstSlotsTransform;

		this.DisplayShelf();
	}

	/*private void InitializeShelf()
	{
		int count = this.ingredientDatabase.ingredients.Count;
		int index;

		GameObject obj;
		Transform tr, sh1, sh2;

		if((count - 1) % 2 != 0) {
			Debug.LogWarning("WARNING : It could be better for the number of ingredient to be even ! The last ingredient will be discarded.");
		}

		obj = new GameObject();
		obj.name = "First Shelf";
		sh1 = obj.transform;
		sh1.parent = slotsTransform;
		sh1.localPosition = Vector3.zero;

		obj = new GameObject();
		obj.name = "Second Shelf";
		sh2 = obj.transform;
		sh2.parent = slotsTransform;
		sh2.localPosition = Vector3.zero;

		for(int i = 0; i < NUMBER_INGREDIENT_BY_SHELF; i++) {

			obj = Instantiate(slotPrefab);
			tr = obj.transform;

			tr.parent = sh1;
			tr.position = new Vector3(
				leftAnchor.position.x + (i % NUMBER_INGREDIENT_BY_ROW) * (SLOT_SIZE + DISTANCE_BETWEEN_INGREDIENT),
				leftAnchor.position.y - (i / NUMBER_INGREDIENT_BY_ROW) * (SLOT_SIZE + DISTANCE_BETWEEN_INGREDIENT),
				0f
			);
		}

		for(int i = 0; i < NUMBER_INGREDIENT_BY_SHELF; i++) {

			obj = Instantiate(slotPrefab);
			tr = obj.transform;

			tr.parent = sh2;
			tr.position = new Vector3(
				leftAnchor.position.x + (i % NUMBER_INGREDIENT_BY_ROW) * (SLOT_SIZE + DISTANCE_BETWEEN_INGREDIENT),
				leftAnchor.position.y - (i / NUMBER_INGREDIENT_BY_ROW) * (SLOT_SIZE + DISTANCE_BETWEEN_INGREDIENT),
				0f
			);
		}
	}*/

	private void DisplayShelf()
	{
		int index;

		for(int i = 0; i < NUMBER_INGREDIENT_BY_SHELF; i++) {
			index = i + shelfIndex * NUMBER_INGREDIENT_BY_SHELF;


		}
	}
}
