using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Engine
{
	public abstract class InteractiveEntity : MonoBehaviour
	{
		protected Transform body;

		public ChemicalElementEntity chemical = null;

		protected virtual void Awake()
		{
			// Initialize variable
			body = transform.Find("Body");

			if(!body) {
				Debug.LogWarning("WARNING : This entity does not have a \"Body\". Is this wanted ?", transform);
			}
		}

		protected virtual void Start()
		{
			// empty
		}

		public override string ToString() => $"{gameObject.name} (Interactive Entity: chemical = {chemical})";
	}
}


