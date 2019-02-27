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
		Fire = 2,
		Water = 4,
		Wind = 8,
		Earth = 16,
		Ice = 32,
		Lightning = 64,
		Magma = 128,
		Steam = 256,
		Snow = 512,
		Sand = 1024
	}


	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ------------------------------------- STANDARD ELEMENT --------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	public class Voidd : ChemicalElementEntity {
		public Voidd(float i = 0f) : base(ChemicalElement.Voidd, i, Voidd.weakness) {}

		protected internal static ChemicalElement[] weakness = Enum.GetValues(typeof(ChemicalElement)) as ChemicalElement[];
	}

	public class Fire : ChemicalElementEntity {
		public Fire(float i = 0f) : base(ChemicalElement.Fire, i, Fire.weakness) {}

		protected internal static ChemicalElement[] weakness = new ChemicalElement[] {
			ChemicalElement.Water,
			ChemicalElement.Lightning
		};
	}

	public class Wind : ChemicalElementEntity {
		public Wind(float i = 0f) : base(ChemicalElement.Wind, i, Wind.weakness) {}

		protected internal static ChemicalElement[] weakness = new ChemicalElement[] {
			ChemicalElement.Fire,
			ChemicalElement.Water
		};
	}

	public class Earth : ChemicalElementEntity {
		public Earth(float i = 0f) : base(ChemicalElement.Earth, i, Earth.weakness) {}

		protected internal static ChemicalElement[] weakness = new ChemicalElement[] {
			ChemicalElement.Wind,
			ChemicalElement.Fire
		};
	}

	public class Lightning : ChemicalElementEntity {
		public Lightning(float i = 0f) : base(ChemicalElement.Lightning, i, Lightning.weakness) {}

		protected internal static ChemicalElement[] weakness = new ChemicalElement[] {
			ChemicalElement.Earth,
			ChemicalElement.Wind
		};
	}

	public class Water : ChemicalElementEntity {
		public Water(float i = 0f) : base(ChemicalElement.Water, i, Water.weakness) {}

		protected internal static ChemicalElement[] weakness = new ChemicalElement[] {
			ChemicalElement.Lightning,
			ChemicalElement.Earth
		};
	}






	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* --------------------------------------- MIX ELEMENTS ----------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	/* ---------------------------------------------------------------------------------------------*/
	public class Magma : ChemicalElementMixEntity {
		public Magma(float i = 0f) : base(ChemicalElement.Magma, i, Magma.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Earth,
			ChemicalElement.Fire
		};
	}

	public class Steam : ChemicalElementMixEntity {
		public Steam(float i = 0f) : base(ChemicalElement.Steam, i, Steam.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Fire,
			ChemicalElement.Water
		};	
	}

	public class Ice : ChemicalElementMixEntity {
		public Ice(float i = 0f) : base(ChemicalElement.Ice, i, Ice.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Wind,
			ChemicalElement.Water
		};
	}

	public class Snow : ChemicalElementMixEntity {
		public Snow(float i = 0f) : base(ChemicalElement.Snow, i, Snow.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Steam,
			ChemicalElement.Ice
		};
	}

	public class Sand : ChemicalElementMixEntity {
		public Sand(float i = 0f) : base(ChemicalElement.Sand, i, Sand.combo) {}

		private static ChemicalElement[] combo = new ChemicalElement[] {
			ChemicalElement.Earth,
			ChemicalElement.Ice
		};
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

		private protected static object[] STANDARD_PARAMS = new object[] {0f};

		private const float MIN_INTENSITY = 0f;
		private const float MAX_INTENSITY = 100f;
		private float _intensity = MIN_INTENSITY;
		public float intensity {
			get { return this._intensity; }
			set { this._intensity = (value > MAX_INTENSITY) ? MAX_INTENSITY : ((value < MIN_INTENSITY) ? MIN_INTENSITY : value); }
		}

		public ChemicalElementEntity(ChemicalElement e, float i, ChemicalElement[] weaknesses) {
			this.type = e;
			this.intensity = i;

			this.InitializeInteractiveEngineData();

			this.SetPrimariesAndWeaknesses(weaknesses);
		}

		private protected virtual void SetPrimariesAndWeaknesses(ChemicalElement[] weaknesses) {
			// Set weaknesses
			if(!this.interactiveEngineData.HasWeaknessesOf(this)) {
				this.interactiveEngineData.SetWeaknessesOf(this, weaknesses);
			}

			// Set elements composition
			if(!this.interactiveEngineData.HasPrimariesOf(this)) {
				this.interactiveEngineData.SetPrimariesOf(this, new ChemicalElement[] {this.type});
			}
		}

		private bool IsStrongAgainst(ChemicalElementEntity other) {
			if(this.interactiveEngineData.HasWinnerBetween(this, other)) {
				return this.interactiveEngineData.IsWinningAgainst(this, other);
			}

			int mywin, hiswin;
			mywin = hiswin = 0;

			foreach(ChemicalElement w in this.interactiveEngineData.GetPrimariesOf(this)) {
				foreach(ChemicalElement e in this.interactiveEngineData.GetPrimariesOf(other)) {
					if(w == e) {
						hiswin ++;
					}
				}
			}
			foreach(ChemicalElement w in this.interactiveEngineData.GetPrimariesOf(other)) {
				foreach(ChemicalElement e in this.interactiveEngineData.GetPrimariesOf(this)) {
					if(w == e) {
						mywin ++;
					}
				}
			}

			bool isMainWinning = (mywin != hiswin && mywin > hiswin);
			this.interactiveEngineData.SetWinnerBetween(this, other, isMainWinning);

			return isMainWinning;
		}

		public virtual ChemicalElementEntity Spawn() {
			// only does that
			return Activator.CreateInstance(this.GetType(), ChemicalElementMixEntity.STANDARD_PARAMS) as ChemicalElementEntity;
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
			if(a.type == b.type) {
				a.intensity = b.intensity = a.intensity + b.intensity;
				return a;
			}
			ChemicalElementEntity m = ChemicalElementMixEntity.MixTwoElement(a, b);
			if(m != null) {
				return m;
			}
			return ChemicalElementEntity.GetWinnerBetween(a, b);
		}

		// ELEMENT * MATERIAL
		public static ChemicalElementEntity operator *(ChemicalMaterialEntity a, ChemicalElementEntity b) => b * a;
		public static ChemicalElementEntity operator *(ChemicalElementEntity a, ChemicalMaterialEntity b) {
			foreach(ChemicalElement e in b.vulnerabilities) {
				if(a.type == e) {
					return a;
				}
			}
			return new Voidd();
		}




		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* -------------------------------------- STATIC METHODS ---------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/		
		private protected static ChemicalElementEntity GetWinnerBetween(ChemicalElementEntity a, ChemicalElementEntity b) {
			if(a.IsStrongAgainst(b)) {
				return a;
			} else if(b.IsStrongAgainst(a)) {
				return b;
			} else if(a.intensity != b.intensity) {
				return (a.intensity > b.intensity) ? a : b;
			} else {
				return new Voidd();
			}
		}
	}







	public abstract class ChemicalElementMixEntity : ChemicalElementEntity
	{
		private protected ChemicalElementMixEntity(ChemicalElement e, float i, ChemicalElement[] recipe) : base(e, i, recipe) {}

		private protected override void SetPrimariesAndWeaknesses(ChemicalElement[] recipe) {
			// Set weaknesses according to recipe
			if(!this.interactiveEngineData.HasWeaknessesOf(this)) {
				this.interactiveEngineData.SetWeaknessesOf(this, GetWeaknessesByDecomposition(recipe));
			}

			// Set elements composition by decomposing recipe in its primary element
			if(!this.interactiveEngineData.HasPrimariesOf(this)) {
				this.interactiveEngineData.SetPrimariesOf(this, GetPrimariesByDecomposition(recipe));
			}
		}

		// does elements in 'a' and 'b' validate this primary element composition
		protected internal bool CouldBeMadeOf(ChemicalElementEntity a, ChemicalElementEntity b) {
			ChemicalElement[] a1 = this.interactiveEngineData.GetPrimariesOf(a);
			ChemicalElement[] a2 = this.interactiveEngineData.GetPrimariesOf(b);
			bool found;

			foreach(ChemicalElement e in this.interactiveEngineData.GetPrimariesOf(this)) {
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
				return Activator.CreateInstance(t, ChemicalElementMixEntity.STANDARD_PARAMS) as ChemicalElementEntity;
			}

			List<ChemicalElementMixEntity> candidates = null;
			ChemicalElementEntity winner = null;

			candidates = _interactiveEngineData.chemicalElementMixEntityPoolList;
			candidates.Clear();

			foreach(ChemicalElementMixEntity mix in ChemicalElementMixEntity.mixes) {
				if(mix.type != a.type && mix.type != b.type && mix.CouldBeMadeOf(a, b)) {
					candidates.Add(mix);
				}
			}

			if(candidates.Count > 0) {
				winner = candidates[0];
				for(int i = 1; i < candidates.Count; i++) {
					winner = GetWinnerBetween(winner, candidates[i]);
				}
			}

			_interactiveEngineData.SetMixOf(a, b, winner);

			return (winner != null) ? winner.Spawn() : null;
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

		// get weaknesses according to recipe element weaknesses
		private static ChemicalElement[] GetWeaknessesByDecomposition(ChemicalElement[] recipe) {
			ChemicalElement[] wk0 = SlowGetWeaknessOf(recipe[0]);
			ChemicalElement[] wk1 = SlowGetWeaknessOf(recipe[1]);
			ChemicalElement[] wk = wk0.Concat(wk1).ToArray();
			List<ChemicalElement> cache = _interactiveEngineData.chemicalElementPoolList;
			bool found;

			cache.Clear();

			foreach(ChemicalElement e in wk) {
				// do not add weakness which are part of our recipe !
				found = false;
				foreach(ChemicalElement a in recipe) {
					if(e == a) {
						found = true;
						break;
					}
				}

				if(!found && !cache.Contains(e)) {
					cache.Add(e);
				}
			}

			return cache.ToArray();
		}

		// get primaries element of a particular element
		private static ChemicalElement[] SlowGetPrimariesOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd: 
				case ChemicalElement.Fire:
				case ChemicalElement.Wind:
				case ChemicalElement.Water:
				case ChemicalElement.Earth:
				case ChemicalElement.Lightning:
					return new ChemicalElement[] {e};
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize this primaries of 'e'
					ent = Activator.CreateInstance(type, ChemicalElementMixEntity.STANDARD_PARAMS) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetPrimariesOf(ent);
				}
			}

			Debug.LogError($"ERROR: Should have not reached this place but we did with '{e}' !");
			return null;
		}

		// get weaknesses of a particular element
		private static ChemicalElement[] SlowGetWeaknessOf(ChemicalElement e) {
			switch(e) {
				case ChemicalElement.Voidd: return Voidd.weakness;
				case ChemicalElement.Fire: return Fire.weakness;
				case ChemicalElement.Wind: return Wind.weakness;
				case ChemicalElement.Water: return Water.weakness;
				case ChemicalElement.Earth: return Earth.weakness;
				case ChemicalElement.Lightning: return Lightning.weakness;
			}

			ChemicalElementMixEntity ent;

			foreach(Type type in ChemicalElementMixEntity.types) {
				if(type.Name == e.ToString()) {
					// this will initialize the weaknesses of 'e'
					ent = Activator.CreateInstance(type, ChemicalElementMixEntity.STANDARD_PARAMS) as ChemicalElementMixEntity;
					return _interactiveEngineData.GetWeaknessesOf(ent);
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
				res[i++] = Activator.CreateInstance(type, ChemicalElementMixEntity.STANDARD_PARAMS) as ChemicalElementMixEntity;
			}

			return res;
		}
	}
}
