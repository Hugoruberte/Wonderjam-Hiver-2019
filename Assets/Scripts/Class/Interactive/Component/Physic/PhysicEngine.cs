using UnityEngine;


namespace Interactive.Engine
{
	public class PhysicEngine
	{
		public PhysicalInteractionEntity InteractionBetween(InteractiveEntity main, InteractiveEntity other, Collision collision)
		{
			PhysicalInteractionEntity interaction;

			interaction = main.physical * other.physical;
			interaction.other = other.gameObject;
			Debug.LogWarning("Need to finish here...");
			// interaction.intensity = collision.blablabla;

			return interaction;
		}
	}
}

