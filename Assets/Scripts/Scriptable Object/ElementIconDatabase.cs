using UnityEngine;
using System.Linq;
// using System.Collections;
using Interactive.Engine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ElementIconDatabase", menuName = "Scriptable Object/Other/ElementIconDatabase", order = 3)]
public class ElementIconDatabase : ScriptableObject
{
	[System.Serializable]
	public struct ChemicalToIcon {
		public ChemicalElement element;
		public Sprite sprite;

		public ChemicalToIcon(ChemicalElement e, Sprite s) {
			this.element = e;
			this.sprite = s;
		}
	}

	public List<ChemicalToIcon> icons = new List<ChemicalToIcon>();

	public Sprite GetIconWith(ChemicalElement e)
	{
		// only does that
		return this.icons.Find(x => x.element == e).sprite;
	}
}