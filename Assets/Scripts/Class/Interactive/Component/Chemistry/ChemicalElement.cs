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
		DraculaSunrise = 128,
		BloodySlimy = 256,
		StickyTear = 512,
		BigIsland = 1024,
		ButtStallion = 2048,
		SlimTonic = 4096,
		PinaAmbrosia = 8192,
		Mojito = 16384,
		Eternitear = 32768,
		Bartinic = 65536,
		Ichor = 131072,
		DevilTear = 262144,
		KingOfSnake = 524288,
		BasiLicorn = 048576, 
		SlimyGod = 2097152,
		RainbowGodtail = 4194304,
		BarmanChor = 8388608,
		AshBullSia = 16777216,
		WingedBull = 33554432,
		BulliSnake = 67108864,
		Kir = 134217728,
		Librid = 268435456,
		HellFire = 536870912,
		TearApart = 1073741824
	}

	public enum Attribute
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
		White,
		Yellow,
		Rainbow,
		Blue
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

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Strong, 3)
		};
	}

	public class BasilicAshes : ChemicalElementEntity {
		public BasilicAshes() : base(ChemicalElement.BasilicAshes, BasilicAshes._attributs, BasilicAshes._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Black;

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Fatal, 3)
		};
	}

	public class Ambrosia : ChemicalElementEntity {
		public Ambrosia() : base(ChemicalElement.Ambrosia, Ambrosia._attributs, Ambrosia._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Yellow;

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Sugar, 3)
		};
	}

	public class GreenSlim : ChemicalElementEntity {
		public GreenSlim() : base(ChemicalElement.GreenSlim, GreenSlim._attributs, GreenSlim._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Green;

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Spicy, 3)
		};
	}

	public class RainbowBull : ChemicalElementEntity {
		public RainbowBull() : base(ChemicalElement.RainbowBull, RainbowBull._attributs, RainbowBull._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Rainbow;

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Sugar, 1),
			new AlcoholAttribute(Attribute.Fatal, 1)
		};
	}

	public class BarmanTear : ChemicalElementEntity {
		public BarmanTear() : base(ChemicalElement.BarmanTear, BarmanTear._attributs, BarmanTear._color) {}

		protected internal static AlcoholColor _color = AlcoholColor.Blue;

		protected internal static AlcoholAttribute[] _attributs = new AlcoholAttribute[] {
			new AlcoholAttribute(Attribute.Strong, 1),
			new AlcoholAttribute(Attribute.Spicy, 1)
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

	public class Ichor : ChemicalElementMixEntity
	{
		public Ichor() : base(ChemicalElement.Ichor, Ichor.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.Ambrosia
		};
	}

	public class BloodySlimy : ChemicalElementMixEntity {
		public BloodySlimy() : base(ChemicalElement.BloodySlimy, BloodySlimy.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.GreenSlim
		};
	}

	public class DevilTear : ChemicalElementMixEntity
	{
		public DevilTear() : base(ChemicalElement.DevilTear, DevilTear.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.BarmanTear
		};
	}

	public class KingOfSnake : ChemicalElementMixEntity { 

		public KingOfSnake() : base(ChemicalElement.KingOfSnake, KingOfSnake.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.BasilicAshes,
			ChemicalElement.GreenSlim
		};
	}

	public class BasiLicorn : ChemicalElementMixEntity
	{

		public BasiLicorn() : base(ChemicalElement.BasiLicorn, BasiLicorn.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.BasilicAshes,
			ChemicalElement.RainbowBull
		};
	}

	
	public class StickyTear : ChemicalElementMixEntity {
		public StickyTear() : base(ChemicalElement.StickyTear, StickyTear.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.GreenSlim,
			ChemicalElement.BarmanTear
		};
	}

	public class SlimyGod : ChemicalElementMixEntity
	{
		public SlimyGod() : base(ChemicalElement.SlimyGod, SlimyGod.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Ambrosia,
			ChemicalElement.GreenSlim
		};
	}

	public class RainbowGodtail : ChemicalElementMixEntity
	{
		public RainbowGodtail() : base(ChemicalElement.RainbowGodtail, RainbowGodtail.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Ambrosia,
			ChemicalElement.RainbowBull
		};
	}

	public class BarmanChor : ChemicalElementMixEntity
	{
		public BarmanChor() : base(ChemicalElement.BarmanChor, BarmanChor.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Ichor,
			ChemicalElement.BarmanTear
		};
	}

	public class AshBullSia : ChemicalElementMixEntity
	{
		public AshBullSia() : base(ChemicalElement.AshBullSia, AshBullSia.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.PinaAmbrosia,
			ChemicalElement.RainbowBull
		};
	}

	public class WingedBull : ChemicalElementMixEntity
	{
		public WingedBull() : base(ChemicalElement.WingedBull, WingedBull.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Ichor,
			ChemicalElement.RainbowBull
		};
	}

	public class BulliSnake : ChemicalElementMixEntity
	{
		public BulliSnake() : base(ChemicalElement.BulliSnake, BulliSnake.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.KingOfSnake,
			ChemicalElement.RainbowBull
		};
	}


	public class HellFire : ChemicalElementMixEntity
	{
		public HellFire() : base(ChemicalElement.HellFire, HellFire.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DraculaSunrise,
			ChemicalElement.SlimyGod
		};
	}

	public class Librid : ChemicalElementMixEntity
	{
		public Librid() : base(ChemicalElement.Librid, Librid.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DraculaSunrise,
			ChemicalElement.RainbowGodtail
		};
	}

	public class Kir : ChemicalElementMixEntity
	{
		public Kir() : base(ChemicalElement.Kir, Kir.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.SlimyGod,
			ChemicalElement.RainbowBull
		};
	}

	public class TearApart : ChemicalElementMixEntity
	{
		public TearApart() : base(ChemicalElement.TearApart, TearApart.combo) { }

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.BasilicAshes,
			ChemicalElement.Ambrosia,
			ChemicalElement.GreenSlim,
			ChemicalElement.RainbowBull,
		};
	}


	public class BigIsland : ChemicalElementMixEntity {
		public BigIsland() : base(ChemicalElement.BigIsland, BigIsland.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.BasilicAshes,
			ChemicalElement.Ambrosia,
			ChemicalElement.GreenSlim ,
			ChemicalElement.RainbowBull,
			ChemicalElement.BarmanTear
		};
	}

	public class ButtStallion : ChemicalElementMixEntity {
		public ButtStallion() : base(ChemicalElement.ButtStallion, ButtStallion.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.DragonBlood,
			ChemicalElement.BasilicAshes,
			ChemicalElement.RainbowBull
		};
	}

	public class SlimTonic : ChemicalElementMixEntity {
		public SlimTonic() : base(ChemicalElement.SlimTonic, SlimTonic.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.StickyTear,
			ChemicalElement.RainbowBull
		};
	}

	public class PinaAmbrosia : ChemicalElementMixEntity {
		public PinaAmbrosia() : base(ChemicalElement.PinaAmbrosia, PinaAmbrosia.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.BasilicAshes,
			ChemicalElement.Ambrosia
		};
	}

	public class Mojito : ChemicalElementMixEntity {
		public Mojito() : base(ChemicalElement.Mojito, Mojito.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.StickyTear,
			ChemicalElement.DragonBlood
		};
	}

	public class Eternitear : ChemicalElementMixEntity {
		public Eternitear() : base(ChemicalElement.Eternitear, Eternitear.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Ambrosia,
			ChemicalElement.RainbowBull,
			ChemicalElement.BarmanTear
		};
	}

	public class Bartinic : ChemicalElementMixEntity {
		public Bartinic() : base(ChemicalElement.Bartinic, Bartinic.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.BasilicAshes,
			ChemicalElement.GreenSlim,
			ChemicalElement.BarmanTear
		};
	}











	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------- ATTRIBUTS ------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	/* ----------------------------------------------------------------------------------------------*/
	[Serializable]
	public struct AlcoholAttribute
	{
		public float intensity;
		public Attribute attribute;

		public AlcoholAttribute(Attribute a, float i)
		{
			this.intensity = i;
			this.attribute = a;
		}

		public AlcoholAttribute(Attribute a, int i)
		{
			this.intensity = (float)i;
			this.attribute = a;
		}

		public override string ToString() => $"{attribute} (intensity = {intensity})";
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
		public ChemicalElement type { get; protected set; }

		public AlcoholAttribute[] attributes { get; protected set; }

		public AlcoholColor[] colors { get; protected set; }

		private protected InteractiveEngineData interactiveEngineData;
		private protected static InteractiveEngineData _interactiveEngineData;

		public ChemicalElementEntity(ChemicalElement e, AlcoholAttribute[] a, AlcoholColor c, ChemicalElement[] r = null) {
			this.type = e;

			this.InitializeInteractiveEngineData();

			this.SetAlcoholAttributesAndAlcoholColor(a, c, r);
		}

		private protected virtual void SetAlcoholAttributesAndAlcoholColor(AlcoholAttribute[] a, AlcoholColor c, ChemicalElement[] recipe) {
			// Set weaknesses according to recipe
			this.attributes = a;
			if(!this.interactiveEngineData.HasAlcoholAttributesOf(this.type)) {
				this.interactiveEngineData.SetAlcoholAttributesOf(this.type, this.attributes);
			}

			this.colors = new AlcoholColor[] {c};
			if(!this.interactiveEngineData.HasColorsOf(this.type)) {
				this.interactiveEngineData.SetColorsOf(this.type, this.colors);
			}

			// Set elements composition by decomposing recipe in its primary element
			if(!this.interactiveEngineData.HasPrimariesOf(this.type)) {
				this.interactiveEngineData.SetPrimariesOf(this.type, new ChemicalElement[] {this.type});
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

		private protected override void SetAlcoholAttributesAndAlcoholColor(AlcoholAttribute[] a, AlcoholColor c, ChemicalElement[] recipe) {
			// Set weaknesses according to recipe
			this.attributes = GetAlcoholAttributesByDecomposition(recipe);
			if(!this.interactiveEngineData.HasAlcoholAttributesOf(this.type)) {
				this.interactiveEngineData.SetAlcoholAttributesOf(this.type, this.attributes);
			}

			this.colors = GetColorsByDecomposition(recipe);
			if(!this.interactiveEngineData.HasColorsOf(this.type)) {
				this.interactiveEngineData.SetColorsOf(this.type, this.colors);
			}

			// Set elements composition by decomposing recipe in its primary element
			if(!this.interactiveEngineData.HasPrimariesOf(this.type)) {
				this.interactiveEngineData.SetPrimariesOf(this.type, GetPrimariesByDecomposition(recipe));
			}
		}

		// does elements in 'a' and 'b' validate this primary element composition
		protected internal bool CanBeMadeOf(ChemicalElementEntity a, ChemicalElementEntity b) {
			ChemicalElement[] a1 = this.interactiveEngineData.GetPrimariesOf(a.type);
			ChemicalElement[] a2 = this.interactiveEngineData.GetPrimariesOf(b.type);
			ChemicalElement[] mine = this.interactiveEngineData.GetPrimariesOf(this.type);
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

		protected internal bool CanBeMadeOf(List<ChemicalElement> es) {
			ChemicalElement[] mine = this.interactiveEngineData.GetPrimariesOf(this.type);
			int count = 0;
			bool found;

			foreach(ChemicalElement e in es) {
				count += this.interactiveEngineData.GetPrimariesOf(e).Length;
			}

			if(count != mine.Length) {
				return false;
			}

			foreach(ChemicalElement e in mine) {
				found = false;

				foreach(ChemicalElement els in es) {
					foreach(ChemicalElement el in this.interactiveEngineData.GetPrimariesOf(els)) {
						if(el == e) {
							found = true;
							break;
						}
					}
					if(found) {
						break;
					}
				}

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
			} else if(b.type == a.type) {
				return a.Spawn();
			}

			if(_interactiveEngineData.HasMixOf(a.type, b.type)) {
				string name = _interactiveEngineData.GetMixOf(a.type, b.type);
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

			if(winner != null) {
				_interactiveEngineData.SetMixOf(a.type, b.type, winner.type);
			} else {
				_interactiveEngineData.SetMixOf(a.type, b.type, ChemicalElement.Voidd);
			}

			return winner?.Spawn();
		}

		protected internal static ChemicalElementEntity MixSeveralElement(List<ChemicalElement> combo) {
			ChemicalElementEntity winner = null;

			foreach(ChemicalElementMixEntity mix in ChemicalElementMixEntity.mixes) {
				if(!combo.Exists(x => x == mix.type) && mix.CanBeMadeOf(combo)) {
					winner = mix;
					break;
				}
			}

			return (winner != null) ? winner.Spawn() : new Voidd();
		}

		// get composition by decomposing 'recipe' in its primary element
		private static ChemicalElement[] GetPrimariesByDecomposition(ChemicalElement[] recipe) {
			List<ChemicalElement> cache = _interactiveEngineData.chemicalElementPoolList;
			cache.Clear();

			foreach(ChemicalElement es in recipe) {
				foreach(ChemicalElement e in SlowGetPrimariesOf(es)) {
					if(!cache.Contains(e)) {
						cache.Add(e);
					}
				}
			}

			return cache.ToArray();
		}

		// get colors according to recipe element colors
		private static AlcoholAttribute[] GetAlcoholAttributesByDecomposition(ChemicalElement[] recipe) {
			AlcoholAttribute at;

			List<AlcoholAttribute> cache = _interactiveEngineData.attributePoolList;
			cache.Clear();

			foreach(ChemicalElement es in recipe) {
				foreach(AlcoholAttribute a in SlowGetAttributesOf(es)) {
					at = cache.Find(x => x.attribute == a.attribute);
					if(at.attribute == Attribute.None) {
						cache.Add(a);
					} else {
						cache.Add(new AlcoholAttribute(at.attribute, (a.intensity + at.intensity) / 2f));
						cache.Remove(at);
					}
				}
			}

			return cache.ToArray();
		}

		// get colors according to recipe element colors
		private static AlcoholColor[] GetColorsByDecomposition(ChemicalElement[] recipe) {
			List<AlcoholColor> cache = _interactiveEngineData.colorPoolList;
			cache.Clear();

			foreach(ChemicalElement es in recipe) {
				cache.AddRange(SlowGetAlcoholColorsOf(es));
			}
			
			return cache.ToArray();
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
					return _interactiveEngineData.GetPrimariesOf(ent.type);
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
				case ChemicalElement.Ambrosia:
					return new AlcoholColor[] {Ambrosia._color};
				case ChemicalElement.GreenSlim :
					return new AlcoholColor[] {GreenSlim._color};
				case ChemicalElement.RainbowBull :
					return new AlcoholColor[] {RainbowBull._color};
				case ChemicalElement.BarmanTear :
					return new AlcoholColor[] {BarmanTear._color};
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetColorsOf(ent.type);
				}
			}

			Debug.LogError($"ERROR: Should have not reached this place but we did with '{e}' !");
			return null;
		}

		// get attributs of a particular element
		private static AlcoholAttribute[] SlowGetAttributesOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd:
					return null;
				case ChemicalElement.DragonBlood:
					return DragonBlood._attributs;
				case ChemicalElement.BasilicAshes:
					return BasilicAshes._attributs;
				case ChemicalElement.Ambrosia:
					return Ambrosia._attributs;
				case ChemicalElement.GreenSlim :
					return GreenSlim._attributs;
				case ChemicalElement.RainbowBull :
					return RainbowBull._attributs;
				case ChemicalElement.BarmanTear :
					return BarmanTear._attributs;
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetAttributesOf(ent.type);
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
