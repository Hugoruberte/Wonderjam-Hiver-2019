using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Engine
{
	public class InteractiveEngine : Singleton<InteractiveEngine>
	{
		public InteractiveEngineData interactiveEngineData;
		
		private static List<InteractiveExtensionEngine> extensions = new List<InteractiveExtensionEngine>();

		private static ChemistryEngine chemistry = new ChemistryEngine();
		private static PhysicEngine physic = new PhysicEngine();

		protected override void Awake()
		{
			base.Awake();

			if(interactiveEngineData == null) {
				Debug.LogWarning("WARNING : You forgot to add an 'InteractiveEngineData' scriptable object to the InteractiveEngine !", transform);
			}

			// Add extension engine :
			extensions.Add(new FoodChainEngine());
		}

		public static void InteractionBetween(InteractiveEntity main, InteractiveEntity other, Collision collision)
		{
			// declaration
			PhysicalInteractionEntity physicalInteraction;
			ChemicalElementEntity chemicalInteraction;

			// interactions
			chemicalInteraction = chemistry.InteractionBetween(main, other);
			physicalInteraction = physic.InteractionBetween(main, other, collision);
			
			// reaction
			Reaction(main, chemicalInteraction, physicalInteraction);
			
			// extensions interaction + reaction
			foreach(InteractiveExtensionEngine ext in extensions) {
				ext.InteractionBetween(main, other);
			}
		}

		public static void Reaction(InteractiveEntity main, ChemicalElementEntity element, PhysicalInteractionEntity interaction)
		{
			InteractiveStatus status;

			// Calculate reaction :
			// 1. Result between 'current physical state' and 'possible element' 
			// For example : Frozen * Fire = Neutral; Water
			status = main.physical * element;
			
			// Update entity interactive status
			main.physical = status.state;
			main.chemical = status.element;

			// main manage its new status && the interaction with the unknown entity
			main.InteractWith(status, interaction);
		}
	}

	public struct InteractiveStatus {
		public PhysicalStateEntity state;
		public ChemicalElementEntity element;
		
		public InteractiveStatus(PhysicalStateEntity s, ChemicalElementEntity e) {
			this.state = s;
			this.element = e;
		}

		public override string ToString() => $"{state}; {element}";
	}
}


