using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Engine
{
	[RequireComponent(typeof(Collider))]
	public abstract class InteractiveEntity : MonoBehaviour
	{
		public Transform myTransform { get; private set; }
		protected Transform body;
		protected Collider myCollider;


		private InteractiveStatus status;

		public PhysicalStateEntity physical {
			get { return this.status.state; }
			set { this.status.state = value; }
		}
		public ChemicalElementEntity chemical {
			get { return this.status.element; }
			set { this.status.element = value; }
		}
		public ChemicalMaterialEntity material { get; protected set; } = ChemicalMaterialEntity.flesh;
		


		private float _life = 100f;
		public float life {
			get { return this._life; }
			set {
				this._life = value;
				this.OnUpdateLife();
			}
		}

		public bool isAlive { get { return (this.life > 0f); }}


		protected delegate void SetOnElement(bool active);
		protected SetOnElement currentSetOnElement;




		protected virtual void Awake()
		{
			// Initialize variable
			myTransform = transform;
			body = myTransform.Find("Body");
			myCollider = GetComponent<Collider>();

			status = new InteractiveStatus(PhysicalStateEntity.neutral, new Voidd());

			if(!body) {
				Debug.LogWarning("WARNING : This entity does not have a \"Body\". Is this wanted ?", myTransform);
			}
		}

		protected virtual void Start()
		{
			// initialize state
			this.InteractWith(this.status, null);

			// initialize cell
			// this.cellable.Initialize(myTransform);
		}

		protected virtual void OnCollisionEnter(Collision other)
		{
			InteractiveEntity ent;

			ent = other.transform.GetComponentInParent<InteractiveEntity>();
			if(ent != null) {
				InteractiveEngine.InteractionBetween(this, ent, other);
			}
		}

		protected void SetInteractiveState(ChemicalElementEntity e, ChemicalMaterialEntity m, PhysicalStateEntity p)
		{
			this.status.state = p;
			this.status.element = e;
			this.material = m;
		}

		// life
		protected virtual void OnUpdateLife() {}

		public virtual void InteractWith(InteractiveStatus s, PhysicalInteractionEntity i) {}

		public override string ToString() => $"{gameObject.name} (Interactive Entity: status = {status} and material = {material})";
	}
}


