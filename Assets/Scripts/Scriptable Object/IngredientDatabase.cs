using UnityEngine;
using System.Linq;
// using System.Collections;
using Interactive.Engine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IngredientDatabase", menuName = "Scriptable Object/Other/IngredientDatabase", order = 3)]
public class IngredientDatabase : ScriptableObject
{
	public List<Ingredient> ingredients = new List<Ingredient>();

	public Sprite GetIconWith(ChemicalElement e)
	{
		// only does that
		return this.ingredients.Find(x => x.element == e).sprite;
	}
}

[System.Serializable]
public struct Ingredient {
	public ChemicalElement element;
	public Sprite sprite;
	public bool isUnlocked;
	[System.NonSerialized] public bool isUsed;

	public Ingredient(Sprite s) {
		this.sprite = s;

		this.element = ChemicalElement.Voidd;
		this.isUnlocked = false;
		this.isUsed = false;
	}

	public Ingredient(Ingredient i, bool used) {
		this.sprite = i.sprite;
		this.element = i.element;
		this.isUnlocked = i.isUnlocked;
		
		this.isUsed = used;
	}

	public Ingredient(bool unlocked, Ingredient i) {
		this.sprite = i.sprite;
		this.element = i.element;
		this.isUsed = i.isUsed;

		this.isUnlocked = unlocked;
	}
}