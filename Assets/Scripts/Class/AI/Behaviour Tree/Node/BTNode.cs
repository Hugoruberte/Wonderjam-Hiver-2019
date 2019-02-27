using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public abstract class BTNode
{
	public BehaviourTree bt = null;
	public BTNode parent = null;
	

	public virtual BTState Tick(BTState overrideState)
	{
		Debug.LogWarning("Should have redefined this 'Tick' !");
		return BTState.Failure;
	}

	public virtual BTState Tick()
	{
		return this.Tick(BTState.None);
	}
}
