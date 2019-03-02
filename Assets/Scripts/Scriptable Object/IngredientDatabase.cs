using UnityEngine;
using System.Linq;
// using System.Collections;
using Interactive.Engine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IngredientDatabase", menuName = "Scriptable Object/Other/IngredientDatabase", order = 3)]
public class IngredientDatabase : ScriptableObject
{
	[System.Serializable]
	public struct Ingredient {
		public ChemicalElement element;
		public Sprite sprite;
		public bool unlocked;

		public Ingredient(Sprite s) {
			this.sprite = s;

			this.element = ChemicalElement.Voidd;
			this.unlocked = false;
		}
	}

	public List<Ingredient> ingredients = new List<Ingredient>();

	public Sprite GetIconWith(ChemicalElement e)
	{
		// only does that
		return this.ingredients.Find(x => x.element == e).sprite;
	}
}