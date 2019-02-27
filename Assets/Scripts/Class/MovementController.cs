using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[System.Serializable]
public abstract class MovementController
{
	public readonly LivingEntity entity;

	public MovementController(LivingEntity e)
	{
		this.entity = e;
	}
}
