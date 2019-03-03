using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;


namespace Interactive.Engine
{
	[Serializable]
	public struct ChemicalToArrayData {
		public ChemicalElement element;
		public ChemicalElement[] array;

		public ChemicalToArrayData(ChemicalElement e, ChemicalElement[] a) {
			this.element = e;
			this.array = a;
		}
	}

	[Serializable]
	public struct ChemicalToAlcoholAttributesData {
		public ChemicalElement element;
		public AlcoholAttribute[] array;

		public ChemicalToAlcoholAttributesData(ChemicalElement e, AlcoholAttribute[] a) {
			this.element = e;
			this.array = a;
		}
	}

	[Serializable]
	public struct ChemicalToColorData {
		public ChemicalElement element;
		public AlcoholColor[] array;

		public ChemicalToColorData(ChemicalElement e, AlcoholColor[] a) {
			this.element = e;
			this.array = a;
		}
	}

	[Serializable]
	public struct IntToChemicalData {
		public int couple;
		public ChemicalElement type;
		public bool empty;

		public IntToChemicalData(int c, ChemicalElement t, bool e = false) {
			this.couple = c;
			this.type = t;
			this.empty = e;
		}
	}



	[CreateAssetMenu(fileName = "InteractiveEngineData", menuName = "Scriptable Object/Other/InteractiveEngineData", order = 3)]
	public class InteractiveEngineData : ScriptableObject
	{
		[NonSerialized] public List<ChemicalElementMixEntity> chemicalElementMixEntityPoolList = new List<ChemicalElementMixEntity>();
		[NonSerialized] public List<ChemicalElement> chemicalElementPoolList = new List<ChemicalElement>();
		[NonSerialized] public List<AlcoholColor> colorPoolList = new List<AlcoholColor>();
		[NonSerialized] public List<AlcoholAttribute> attributePoolList = new List<AlcoholAttribute>();

		[HideInInspector] public List<ChemicalToArrayData> primaries = new List<ChemicalToArrayData>();
		[HideInInspector] public List<ChemicalToAlcoholAttributesData> attributes = new List<ChemicalToAlcoholAttributesData>();
		[HideInInspector] public List<ChemicalToColorData> colors = new List<ChemicalToColorData>();
		[HideInInspector] public List<IntToChemicalData> couples = new List<IntToChemicalData>();

		public StringBuilder stringBuilder = new StringBuilder();
		private const string voiddString = "Interactive.Engine.Voidd";




		public _ChemicalElementEntity GetChemicalElementDataWithEnum(ChemicalElement e)
		{
			// only does that
			return new _ChemicalElementEntity(e, this.GetColorsOf(e), this.GetAttributesOf(e));
		}

		public Color GetColorWithAlcoholColor(AlcoholColor c) {
			switch(c) {
				case AlcoholColor.Red:
					return Color.red;
				case AlcoholColor.Green:
					return Color.green;
				case AlcoholColor.Black:
					return Color.black;
				case AlcoholColor.White:
					return Color.white;
				case AlcoholColor.Yellow:
					return Color.yellow;
				case AlcoholColor.Pink:
					return new Color32(255, 38, 226, 255);
				case AlcoholColor.Blue:
					return Color.blue;
			}

			return Color.white;
		}




		public bool HasPrimariesOf(ChemicalElement ent)
		{
			// only does that
			return this.primaries.Exists(x => x.element == ent);
		}
		public void SetPrimariesOf(ChemicalElement ent, ChemicalElement[] ps)
		{
			this.primaries.Add(new ChemicalToArrayData(ent, ps));
			this.primaries.Sort(CompareChemicalToArrayData);
		}
		public ChemicalElement[] GetPrimariesOf(ChemicalElement ent)
		{
			ChemicalToArrayData data = this.primaries.Find(x => x.element == ent);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasAlcoholAttributesOf(ChemicalElement ent)
		{
			// only does that
			return this.attributes.Exists(x => x.element == ent);
		}
		public void SetAlcoholAttributesOf(ChemicalElement ent, AlcoholAttribute[] ats)
		{
			this.attributes.Add(new ChemicalToAlcoholAttributesData(ent, ats));
			this.attributes.Sort(CompareChemicalToAlcoholAttributesData);
		}
		public AlcoholAttribute[] GetAttributesOf(ChemicalElement ent)
		{
			ChemicalToAlcoholAttributesData data = this.attributes.Find(x => x.element == ent);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasColorsOf(ChemicalElement ent)
		{
			// only does that
			return this.colors.Exists(x => x.element == ent);
		}
		public void SetColorsOf(ChemicalElement ent, AlcoholColor[] acs)
		{
			this.colors.Add(new ChemicalToColorData(ent, acs));
			this.colors.Sort(CompareChemicalToColorsData);
		}
		public AlcoholColor[] GetColorsOf(ChemicalElement ent)
		{
			ChemicalToColorData data = this.colors.Find(x => x.element == ent);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasMixOf(ChemicalElement a, ChemicalElement b)
		{
			int couple = (int)(a | b);
			return this.couples.Exists(x => x.couple == couple);
		}
		public void SetMixOf(ChemicalElement a, ChemicalElement b, ChemicalElement ent)
		{
			int couple = (int)(a | b);
			if(!this.couples.Exists(x => x.couple == couple)) {
				if(ent == ChemicalElement.Voidd) {
					this.couples.Add(new IntToChemicalData(couple, ent, true));
				} else {
					this.couples.Add(new IntToChemicalData(couple, ent));
				}
			} else {
				Debug.LogWarning($"WARNING : This couple ({a} + {b}) is already registered ! Check it out for optimization !");
			}
		}
		public string GetMixOf(ChemicalElement a, ChemicalElement b)
		{
			int couple = (int)(a | b);
			IntToChemicalData data = this.couples.Find(x => x.couple == couple);
			if(data.couple > 0) {
				if(data.empty) {
					return null;
				}
				this.stringBuilder.Clear();
				this.stringBuilder.Append("Interactive.Engine.").Append(data.type.ToString());
				return this.stringBuilder.ToString();
			} else {
				Debug.LogWarning($"WARNING : This couple ({a} + {b}) is not yet registered ! Check it out !");
				return voiddString;
			}
		}





		private static int CompareChemicalToArrayData(ChemicalToArrayData x, ChemicalToArrayData y)
		{
			int xe = (int)x.element;
			int ye = (int)y.element;

			if(xe > ye) {
				return 1;
			} else if(xe < ye) {
				return -1;
			} else {
				return 0;
			}
		}

		private static int CompareChemicalToAlcoholAttributesData(ChemicalToAlcoholAttributesData x, ChemicalToAlcoholAttributesData y)
		{
			int xe = (int)x.element;
			int ye = (int)y.element;

			if(xe > ye) {
				return 1;
			} else if(xe < ye) {
				return -1;
			} else {
				return 0;
			}
		}

		private static int CompareChemicalToColorsData(ChemicalToColorData x, ChemicalToColorData y)
		{
			int xe = (int)x.element;
			int ye = (int)y.element;

			if(xe > ye) {
				return 1;
			} else if(xe < ye) {
				return -1;
			} else {
				return 0;
			}
		}
	}
}

public struct _ChemicalElementEntity {
	public ChemicalElement type;
	public AlcoholColor[] colors;
	public AlcoholAttribute[] attributes;

	public _ChemicalElementEntity(ChemicalElement t, AlcoholColor[] c, AlcoholAttribute[] a) {
		this.type = t;
		this.colors = c;
		this.attributes = a;
	}
}