using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;


namespace Interactive.Engine
{
	public enum ChemicalElement
	{
		Voidd = 1,
		DragonBlood = 2,
		BasilicAshes = 4,
		Ambrosia = 8,
		GreenSlim = 16,
		RainbowBull = 32,
		BarmanTear = 64,
		DraculaSunrise = 128
	}

	public enum AlcoholAttribute
	{
		None = 0,
		Fatal,
		Spicy,
		Sugar,
		Strong
	}

	public enum AlcoholColor
	{
		None = 0,
		Red,
		Green,
		Black,
		White
	}


	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ------------------------------------- STANDARD ELEMENT --------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	public class Voidd : ChemicalElementEntity {
		public Voidd() : base(ChemicalElement.Voidd, null, Voidd._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.None;
	}

	public class DragonBlood : ChemicalElementEntity {
		public DragonBlood() : base(ChemicalElement.DragonBlood, DragonBlood._attributs, DragonBlood._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Red;

		protected internal static Attribute[] _attributs = new Attribute[] {
			new Attribute(AlcoholAttribute.Strong, 3),
			new Attribute(AlcoholAttribute.Fatal, 2)
		};
	}

	public class BasilicAshes : ChemicalElementEntity {
		public BasilicAshes() : base(ChemicalElement.BasilicAshes, BasilicAshes._attributs, BasilicAshes._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Black;

		protected internal static Attribute[] _attributs = new Attribute[] {
			new Attribute(AlcoholAttribute.Fatal, 2),
			new Attribute(AlcoholAttribute.Spicy, 1)
		};
	}






	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* --------------------------------------- MIX ELEMENTS ----------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	public class DraculaSunrise : ChemicalElementMixEntity {
		public DraculaSunrise() : base(ChemicalElement.DraculaSunrise, DraculaSunrise.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.BasilicAshes
		};
	}











	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------- ATTRIBUTS ------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	public struct Attribute
	{
		public float intensity;
		public AlcoholAttribute attribute;

		public Attribute(AlcoholAttribute a, float i)
		{
			this.intensity = i;
			this.attribute = a;
		}

		public Attribute(AlcoholAttribute a, int i)
		{
			this.intensity = (float)i;
			this.attribute = a;
		}
	}



















































	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------- DO NOT LOOK AT THESE, IT IS SCARY ------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	public abstract class ChemicalElementEntity
	{
		public readonly ChemicalElement type;

		private protected InteractiveEngineData interactiveEngineData;
		private protected static InteractiveEngineData _interactiveEngineData;

		public ChemicalElementEntity(ChemicalElement e, Attribute[] a, AlcoholColor c, ChemicalElement[] r = null) {
			this.type = e;

			this.InitializeInteractiveEngineData();

			this.SetAttributesAndAlcoholColor(a, c, r);
		}

		private protected virtual void SetAttributesAndAlcoholColor(Attribute[] a, AlcoholColor c, ChemicalElement[] recipe) {
			// Set weaknesses according to recipe
			if(!this.interactiveEngineData.HasAttributesOf(this)) {
				this.interactiveEngineData.SetAttributesOf(this, a);
			}

			if(!this.interactiveEngineData.HasColorsOf(this)) {
				this.interactiveEngineData.SetColorsOf(this, new AlcoholColor[] {c});
			}

			// Set elements composition by decomposing recipe in its primary element
			if(!this.interactiveEngineData.HasPrimariesOf(this)) {
				this.interactiveEngineData.SetPrimariesOf(this, new ChemicalElement[] {this.type});
			}
		}

		public virtual ChemicalElementEntity Spawn() {
			// only does that
			return Activator.CreateInstance(this.GetType()) as ChemicalElementEntity;
		}

		private void InitializeInteractiveEngineData() {
			if(_interactiveEngineData != null) {
				this.interactiveEngineData = _interactiveEngineData;
				return;
			}

			InteractiveEngine engine = MonoBehaviour.FindObjectOfType(typeof(InteractiveEngine)) as InteractiveEngine;
			this.interactiveEngineData = engine.interactiveEngineData;
			_interactiveEngineData = this.interactiveEngineData;
		}

		public override string ToString() => $"{this.type}";




		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------- * OPERATOR -----------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		// ELEMENT * ELEMENT
		public static ChemicalElementEntity operator *(ChemicalElementEntity a, ChemicalElementEntity b) {
			ChemicalElementEntity m = ChemicalElementMixEntity.MixTwoElement(a, b);

			return (m != null) ? m : new Voidd();
		}
	}







	public abstract class ChemicalElementMixEntity : ChemicalElementEntity
	{
		private protected ChemicalElementMixEntity(ChemicalElement e, ChemicalElement[] c) : base(e, null, AlcoholColor.None, c) {}

		private protected override void SetAttributesAndAlcoholColor(Attribute[] a, AlcoholColor c, ChemicalElement[] recipe) {
			// Set weaknesses according to recipe
			if(!this.interactiveEngineData.HasAttributesOf(this)) {
				this.interactiveEngineData.SetAttributesOf(this, GetAttributesByDecomposition(recipe));
			}

			if(!this.interactiveEngineData.HasColorsOf(this)) {
				this.interactiveEngineData.SetColorsOf(this, GetColorsByDecomposition(recipe));
			}

			// Set elements composition by decomposing recipe in its primary element
			if(!this.interactiveEngineData.HasPrimariesOf(this)) {
				this.interactiveEngineData.SetPrimariesOf(this, GetPrimariesByDecomposition(recipe));
			}
		}

		// does elements in 'a' and 'b' validate this primary element composition
		protected internal bool CanBeMadeOf(ChemicalElementEntity a, ChemicalElementEntity b) {
			ChemicalElement[] a1 = this.interactiveEngineData.GetPrimariesOf(a);
			ChemicalElement[] a2 = this.interactiveEngineData.GetPrimariesOf(b);
			ChemicalElement[] mine = this.interactiveEngineData.GetPrimariesOf(this);
			bool found;

			if(mine.Length != a1.Length + a2.Length) {
				return false;
			}

			foreach(ChemicalElement e in mine) {
				found = false;

				// search in first array
				foreach(ChemicalElement k in a1) {
					if(e == k) {
						found = true;
						break;
					}
				}
				// if not found in first array then
				if(!found) {
					// search in second array
					foreach(ChemicalElement k in a2) {
						if(e == k) {
							found = true;
							break;
						}
					}
				}
				// if not found in either one of them
				if(!found) {
					return false;
				}
			}
			return true;
		}

		

		









		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* -------------------------------------- STATIC METHODS ---------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		private static ChemicalElementMixEntity[] _mixes = null;
		private static ChemicalElementMixEntity[] mixes {
			get {
				if(_mixes == null) {
					_mixes = GetAllMixes();
				}
				return _mixes;
			}
		}
		private static Type[] _types = null;
		private static Type[] types {
			get {
				if(_types == null) {
					_types = GetAllTypes();
				}
				return _types;
			}
		}

		protected internal static ChemicalElementEntity MixTwoElement(ChemicalElementEntity a, ChemicalElementEntity b) {
			if(a.type == ChemicalElement.Voidd) {
				return b;
			} else if(b.type == ChemicalElement.Voidd) {
				return a;
			}

			if(_interactiveEngineData.HasMixOf(a, b)) {
				string name = _interactiveEngineData.GetMixOf(a, b);
				if(name == null) {
					return null;
				}
				Type t = Type.GetType(name);
				return Activator.CreateInstance(t) as ChemicalElementEntity;
			}

			ChemicalElementEntity winner = null;

			foreach(ChemicalElementMixEntity mix in ChemicalElementMixEntity.mixes) {
				if(mix.type != a.type && mix.type != b.type && mix.CanBeMadeOf(a, b)) {
					winner = mix;
					break;
				}
			}

			_interactiveEngineData.SetMixOf(a, b, winner);

			return winner?.Spawn();
		}

		// get composition by decomposing 'recipe' in its primary element
		private static ChemicalElement[] GetPrimariesByDecomposition(ChemicalElement[] recipe) {
			ChemicalElement[] es0 = SlowGetPrimariesOf(recipe[0]);
			ChemicalElement[] es1 = SlowGetPrimariesOf(recipe[1]);
			List<ChemicalElement> cache = _interactiveEngineData.chemicalElementPoolList;

			cache.Clear();

			foreach(ChemicalElement e in es0) {
				if(!cache.Contains(e)) {
					cache.Add(e);
				}
			}
			foreach(ChemicalElement e in es1) {
				if(!cache.Contains(e)) {
					cache.Add(e);
				}
			}

			return cache.ToArray();
		}

		// get colors according to recipe element colors
		private static Attribute[] GetAttributesByDecomposition(ChemicalElement[] recipe) {
			Attribute[] a0 = SlowGetAttributesOf(recipe[0]);
			Attribute[] a1 = SlowGetAttributesOf(recipe[1]);
			Attribute at;

			List<Attribute> cache = _interactiveEngineData.attributePoolList;
			cache.Clear();

			foreach(Attribute a in a0) {
				at = cache.Find(x => x.attribute == a.attribute);
				if(at.attribute == AlcoholAttribute.None) {
					cache.Add(a);
				} else {
					at.intensity = (a.intensity + at.intensity) / 2f;
				}
			}

			return cache.ToArray();
		}

		// get colors according to recipe element colors
		private static AlcoholColor[] GetColorsByDecomposition(ChemicalElement[] recipe) {
			AlcoholColor[] wk0 = SlowGetAlcoholColorsOf(recipe[0]);
			AlcoholColor[] wk1 = SlowGetAlcoholColorsOf(recipe[1]);
			
			return wk0.Concat(wk1).ToArray();
		}





		// get primaries element of a particular element
		private static ChemicalElement[] SlowGetPrimariesOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd:
				case ChemicalElement.DragonBlood:
				case ChemicalElement.BasilicAshes:
				case ChemicalElement.Ambrosia:
				case ChemicalElement.GreenSlim :
				case ChemicalElement.RainbowBull :
				case ChemicalElement.BarmanTear :
					return new ChemicalElement[] {e};
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetPrimariesOf(ent);
				}
			}

			Debug.LogError($"ERROR: Should have not reached this place but we did with '{e}' !");
			return null;
		}

		// get colors of a particular element
		private static AlcoholColor[] SlowGetAlcoholColorsOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd:
					return new AlcoholColor[] {Voidd._color};
				case ChemicalElement.DragonBlood:
					return new AlcoholColor[] {DragonBlood._color};
				case ChemicalElement.BasilicAshes:
					return new AlcoholColor[] {BasilicAshes._color};
				/*case ChemicalElement.Ambrosia:
					return new AlcoholColor[] {Ambrosia._color};
				case ChemicalElement.GreenSlim :
					return new AlcoholColor[] {GreenSlim._color};
				case ChemicalElement.RainbowBull :
					return new AlcoholColor[] {RainbowBull._color};
				case ChemicalElement.BarmanTear :
					return new AlcoholColor[] {BarmanTear._color};*/
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetColorsOf(ent);
				}
			}

			Debug.LogError($"ERROR: Should have not reached this place but we did with '{e}' !");
			return null;
		}

		// get attributs of a particular element
		private static Attribute[] SlowGetAttributesOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd:
					return null;
				case ChemicalElement.DragonBlood:
					return DragonBlood._attributs;
				case ChemicalElement.BasilicAshes:
					return BasilicAshes._attributs;
				/*case ChemicalElement.Ambrosia:
					return Ambrosia._attributs;
				case ChemicalElement.GreenSlim :
					return GreenSlim._attributs;
				case ChemicalElement.RainbowBull :
					return RainbowBull._attributs;
				case ChemicalElement.BarmanTear :
					return BarmanTear._attributs;*/
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetAttributesOf(ent);
				}
			}

			Debug.LogError($"ERROR: Should have not reached this place but we did with '{e}' !");
			return null;
		}






		private static Type[] GetAllTypes() {
			Type t;
			t = typeof(ChemicalElementMixEntity);
			return Assembly.GetAssembly(t).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(t)).ToArray();
		}

		private static ChemicalElementMixEntity[] GetAllMixes() {
			int i;
			Type[] ts;
			ChemicalElementMixEntity[] res;

			ts = ChemicalElementMixEntity.types;
			res = new ChemicalElementMixEntity[ts.Length];
			i = 0;

			foreach(Type type in ts) {
				res[i++] = Activator.CreateInstance(type) as ChemicalElementMixEntity;
			}

			return res;
		}
	}
}
