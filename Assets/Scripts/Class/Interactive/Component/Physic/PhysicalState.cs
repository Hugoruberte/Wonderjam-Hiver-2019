using UnityEngine;


namespace Interactive.Engine
{
	public abstract class PhysicalStateEntity
	{
		protected ChemicalElement[] weakness = null;
		protected ChemicalElement[] strengths = null;

		protected PhysicalInteractionEntity reaction { private get; set; }
		protected int priority { private get; set; }
		protected InteractiveStatus? weakStatus = null;
		
		public InteractiveStatus InContactWith(ChemicalElementEntity e) {
			if(this.weakness != null) {
				foreach(ChemicalElement c in this.weakness) {
					if(c == e.type) {
						return (weakStatus == null) ? new InteractiveStatus(neutral, e) : (InteractiveStatus)this.weakStatus;
					}
				}
			}
			if(this.strengths != null) {
				foreach(ChemicalElement c in this.strengths) {
					if(c == e.type) {
						return new InteractiveStatus(this, new Voidd());
					}
				}
			}
			return new InteractiveStatus(this, e);
		}

		public override string ToString() => this.GetType().Name;




		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------- * OPERATOR -----------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		// STATE * STATE
		public static PhysicalInteractionEntity operator *(PhysicalStateEntity a, PhysicalStateEntity b) {
			return (a.priority > b.priority) ? a.reaction : b.reaction;
		}

		// STATE * ELEMENT
		public static InteractiveStatus operator *(PhysicalStateEntity a, ChemicalElementEntity b) => b * a;
		public static InteractiveStatus operator *(ChemicalElementEntity a, PhysicalStateEntity b) {
			return (b.weakness == null) ? new InteractiveStatus(b, a) : b.InContactWith(a);
		}


		


		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ----------------------------------- PHYSICAL INTERACTION ------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		/* ---------------------------------------------------------------------------------------------*/
		public static PhysicalStateEntity neutral = new Neutral();
		private class Neutral : PhysicalStateEntity {
			public Neutral() {
				this.reaction = new Contact();
				this.priority = 10;
			}
		}

		public static PhysicalStateEntity explosive = new Explosive();
		private class Explosive : PhysicalStateEntity {
			public Explosive() {
				this.reaction = new Explosion();
				this.priority = 100;

				this.weakness = new ChemicalElement[] {
					ChemicalElement.Water,
					ChemicalElement.Wind
				};
			}
		}

		public static PhysicalStateEntity frozen = new Frozen();
		private class Frozen : PhysicalStateEntity {
			public Frozen() {
				this.reaction = new Contact();
				this.priority = 20;

				this.weakStatus = new InteractiveStatus(neutral, new Water()); 
				this.weakness = new ChemicalElement[] {
					ChemicalElement.Fire,
					ChemicalElement.Steam
				};

				this.strengths = new ChemicalElement[] {
					ChemicalElement.Ice
				};
			}
		}

		public static PhysicalStateEntity burn = new Burn();
		private class Burn : PhysicalStateEntity {
			public Burn() {
				this.reaction = new Contact();
				this.priority = 20;

				this.weakStatus = new InteractiveStatus(neutral, new Steam()); 
				this.weakness = new ChemicalElement[] {
					ChemicalElement.Water
				};

				this.strengths = new ChemicalElement[] {
					ChemicalElement.Fire
				};
			}
		}
	}
}