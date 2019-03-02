using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	public struct ChemicalToAttributesData {
		public ChemicalElement element;
		public Attribute[] array;

		public ChemicalToAttributesData(ChemicalElement e, Attribute[] a) {
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
		[HideInInspector] public List<ChemicalElementMixEntity> chemicalElementMixEntityPoolList = new List<ChemicalElementMixEntity>();
		[HideInInspector] public List<ChemicalElement> chemicalElementPoolList = new List<ChemicalElement>();
		[HideInInspector] public List<Attribute> attributePoolList = new List<Attribute>();

		[HideInInspector] public List<ChemicalToArrayData> primaries = new List<ChemicalToArrayData>();
		[HideInInspector] public List<ChemicalToAttributesData> attributes = new List<ChemicalToAttributesData>();
		[HideInInspector] public List<ChemicalToColorData> colors = new List<ChemicalToColorData>();
		[HideInInspector] public List<IntToChemicalData> couples = new List<IntToChemicalData>();

		private StringBuilder stringBuilder = new StringBuilder();
		private const string voiddString = "Interactive.Engine.Voidd";




		public bool HasPrimariesOf(ChemicalElementEntity ent)
		{
			// only does that
			return this.primaries.Exists(x => x.element == ent.type);
		}
		public void SetPrimariesOf(ChemicalElementEntity ent, ChemicalElement[] ps)
		{
			this.primaries.Add(new ChemicalToArrayData(ent.type, ps));
			this.primaries.Sort(CompareChemicalToArrayData);
		}
		public ChemicalElement[] GetPrimariesOf(ChemicalElementEntity ent)
		{
			ChemicalToArrayData data = this.primaries.Find(x => x.element == ent.type);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasAttributesOf(ChemicalElementEntity ent)
		{
			// only does that
			return this.attributes.Exists(x => x.element == ent.type);
		}
		public void SetAttributesOf(ChemicalElementEntity ent, Attribute[] ats)
		{
			this.attributes.Add(new ChemicalToAttributesData(ent.type, ats));
			this.attributes.Sort(CompareChemicalToAttributesData);
		}
		public Attribute[] GetAttributesOf(ChemicalElementEntity ent)
		{
			ChemicalToAttributesData data = this.attributes.Find(x => x.element == ent.type);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasColorsOf(ChemicalElementEntity ent)
		{
			// only does that
			return this.colors.Exists(x => x.element == ent.type);
		}
		public void SetColorsOf(ChemicalElementEntity ent, AlcoholColor[] acs)
		{
			this.colors.Add(new ChemicalToColorData(ent.type, acs));
			this.colors.Sort(CompareChemicalToColorsData);
		}
		public AlcoholColor[] GetColorsOf(ChemicalElementEntity ent)
		{
			ChemicalToColorData data = this.colors.Find(x => x.element == ent.type);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
				return null;
			}
		}




		public bool HasMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		{
			int couple = (int)(a.type | b.type);
			return this.couples.Exists(x => x.couple == couple);
		}
		public void SetMixOf(ChemicalElementEntity a, ChemicalElementEntity b, ChemicalElementEntity ent)
		{
			int couple = (int)(a.type | b.type);
			if(!this.couples.Exists(x => x.couple == couple)) {
				if(ent == null) {
					this.couples.Add(new IntToChemicalData(couple, ChemicalElement.Voidd, true));
				} else {
					this.couples.Add(new IntToChemicalData(couple, ent.type));
				}
			} else {
				Debug.LogWarning($"WARNING : This couple ({a} + {b}) is already registered ! Check it out for optimization !");
			}
		}
		public string GetMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		{
			int couple = (int)(a.type | b.type);
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

		private static int CompareChemicalToAttributesData(ChemicalToAttributesData x, ChemicalToAttributesData y)
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


