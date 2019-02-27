using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTWait : BTLeaf
{
	private float duration;
	private float target;

	public BTWait(float duration)
	{
		this.duration = duration;
	}

	public sealed override BTState Tick()
	{
		// init
		if(target < 0f) {
			target = Time.time + duration;
		}

		// process
		if(Time.time >= target) {
			target = -1f;
			return BTState.Success;
		}
		else {
			return BTState.Running;
		}
	}
}
