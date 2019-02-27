using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTInverter : BTDecorator
{
	public BTInverter(BTNode c) : base(c) {}

	public sealed override BTState Tick()
	{
		switch(child.Tick())
		{
			case BTState.Success:
				return BTState.Failure;

			case BTState.Failure:
				return BTState.Success;

			case BTState.Running:
				return BTState.Running;

			default:
				return BTState.Failure;
		}
	}
}
