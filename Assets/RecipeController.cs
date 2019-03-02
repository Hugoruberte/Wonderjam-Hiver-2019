using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class RecipeController : Singleton<RecipeController>
{
	private List<ChemicalElementEntity> combo = new List<ChemicalElementEntity>();

	private float length;
	private float height;
	private float widthOfSlot { get { return height - 2 * distanceBetweenIngredient; }}
	private float left;

	public GameObject slotPrefab;
	public Transform mainTransform;
	public Transform resultTransform;
	public Transform recipeSlotsTransform;

	public float distanceBetweenIngredient = 0.5f;
	public float distanceBeforeResult = 2f;




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
		
		this.UpdateDisplay();
	}

	

	private void UpdateDisplay()
	{
		this.height = mainTransform.localPosition.y;
		this.length = (this.combo.Count + 1) * (distanceBetweenIngredient + widthOfSlot) + distanceBeforeResult;
		this.left = mainTransform.position.x - (this.length / 2f);

		this.UpdateComboDisplay();
		this.UpdateResultDisplay();
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
			tr.position = new Vector3(this.left + i * (distanceBetweenIngredient + widthOfSlot) + distanceBetweenIngredient + (widthOfSlot / 2f), tr.position.y, tr.position.z);
		}
	}
	private void UpdateResultDisplay()
	{
		resultTransform.position = new Vector3(this.left + this.combo.Count * (distanceBetweenIngredient+widthOfSlot) + distanceBeforeResult + (widthOfSlot/2f), resultTransform.position.y, resultTransform.position.z);
	}
}
