using System.Collections.Generic;


namespace Interactive.Engine
{
	public abstract class ChemicalMaterialEntity
	{
		public readonly List<ChemicalElement> vulnerabilities = new List<ChemicalElement>();

		public override string ToString() => this.GetType().Name;




		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ------------------------------------ STANDARD MATERIALS -------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		public static ChemicalMaterialEntity flesh = new Flesh();
		private class Flesh : ChemicalMaterialEntity {
			public Flesh() {
				this.vulnerabilities.AddRange(new ChemicalElement[]{
					ChemicalElement.Fire,
					ChemicalElement.Lightning
				});
			}
		}

		public static ChemicalMaterialEntity wood = new Wood();
		private class Wood : ChemicalMaterialEntity {
			public Wood() {
				this.vulnerabilities.AddRange(new ChemicalElement[]{
					ChemicalElement.Fire
				});
			}
		}

		public static ChemicalMaterialEntity grass = new Grass();
		private class Grass : ChemicalMaterialEntity {
			public Grass() {
				this.vulnerabilities.AddRange(new ChemicalElement[]{
					ChemicalElement.Fire,
					ChemicalElement.Water,
					ChemicalElement.Magma
				});
			}
		}

		public static ChemicalMaterialEntity stone = new Stone();
		private class Stone : ChemicalMaterialEntity {
			public Stone() {
				this.vulnerabilities.AddRange(new ChemicalElement[]{
					ChemicalElement.Magma
				});
			}
		}

		public static ChemicalMaterialEntity metal = new Metal();
		private class Metal : ChemicalMaterialEntity {
			public Metal() {
				this.vulnerabilities.AddRange(new ChemicalElement[]{
					ChemicalElement.Lightning
				});
			}
		}
	}
}
