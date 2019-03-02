using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace Interactive.Engine
{
	public class InteractiveEngine : Singleton<InteractiveEngine>
	{
		public InteractiveEngineData interactiveEngineData;
		public IngredientDatabase ingredientDatabase;
		
		private static ChemistryEngine chemistry = new ChemistryEngine();

		protected override void Awake()
		{
			base.Awake();

			if(interactiveEngineData == null) {
				Debug.LogWarning("WARNING : You forgot to add an 'InteractiveEngineData' scriptable object to the InteractiveEngine !", transform);
			}
		}

		public static void InteractionBetween(InteractiveEntity main, InteractiveEntity other)
		{
			main.chemical = chemistry.InteractionBetween(main, other);
		}

		public static ChemicalElementEntity[] GetAllChemicalEntity()
		{
			int i;
			Type t;
			Type[] ts;
			ChemicalElementEntity[] res;

			t = typeof(ChemicalElementEntity);
			ts = Assembly.GetAssembly(t).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(t)).ToArray();
			res = new ChemicalElementEntity[ts.Length];
			i = 0;

			foreach(Type type in ts) {
				res[i++] = Activator.CreateInstance(type) as ChemicalElementEntity;
			}

			return res;
		}

		public static ChemicalElementEntity GetCocktailFrom(List<ChemicalElement> combo)
		{
			return ChemicalElementMixEntity.MixSeveralElement(combo);
		}
	}
}


