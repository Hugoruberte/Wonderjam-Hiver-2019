using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Engine
{
	public class InteractiveEngine : Singleton<InteractiveEngine>
	{
		public InteractiveEngineData interactiveEngineData;
		
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
	}
}


