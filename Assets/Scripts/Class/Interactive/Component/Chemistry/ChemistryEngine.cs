using UnityEngine;


namespace Interactive.Engine
{
	public class ChemistryEngine
	{
		public ChemicalElementEntity InteractionBetween(InteractiveEntity main, InteractiveEntity other)
		{			
			return (main.chemical * other.chemical) * main.material;
		}

		public void SetEntityWithChemical(InteractiveEntity main, InteractiveEntity.SetOnElement setOnElement)
		{
			if(setOnElement != null) {
				setOnElement(false);
				setOnElement = null;
			}

			switch(main.chemical.type) {

				case ChemicalElement.Voidd:
					setOnElement += main.SetOnVoidd;
					break;

				case ChemicalElement.Fire:
					setOnElement += main.SetOnFire;
					break;

				case ChemicalElement.Water:
					setOnElement += main.SetOnWater;
					break;

				case ChemicalElement.Wind:
					setOnElement += main.SetOnWind;
					break;

				case ChemicalElement.Earth:
					setOnElement += main.SetOnEarth;
					break;

				case ChemicalElement.Ice:
					setOnElement += main.SetOnIce;
					break;

				case ChemicalElement.Lightning:
					setOnElement += main.SetOnLightning;
					break;

				case ChemicalElement.Magma:
					setOnElement += main.SetOnMagma;
					break;

				case ChemicalElement.Steam:
					setOnElement += main.SetOnSteam;
					break;

				default:
					Debug.LogWarning($"WARNING: The callback for this element ({main.chemical.type}) has not been set yet !");
					break;
			}

			setOnElement(true);
		}
	}
}
