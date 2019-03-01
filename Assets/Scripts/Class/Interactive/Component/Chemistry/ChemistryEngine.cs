using UnityEngine;


namespace Interactive.Engine
{
	public class ChemistryEngine
	{
		public ChemicalElementEntity InteractionBetween(InteractiveEntity main, InteractiveEntity other)
		{
			// only does that
			return (main.chemical * other.chemical) * main.material;
		}
	}
}
