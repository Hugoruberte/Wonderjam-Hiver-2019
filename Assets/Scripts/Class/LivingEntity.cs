using UnityEngine;
using Interactive.Engine;

[RequireComponent(typeof(Rigidbody))]
public abstract class LivingEntity : InteractiveEntity
{
	// standard variable, not necessarily used
	[HideInInspector] public float speed = 2;
	[HideInInspector] public int maxStepDistance = 2;
	[HideInInspector] public float rangeOfView = 5f;

	// AI
	[HideInInspector] public UtilityAIBehaviour behaviour;

	[HideInInspector] public bool isTired = false;
	[HideInInspector] public bool isHungry = false;
	[HideInInspector] public bool isScared = false;
}
