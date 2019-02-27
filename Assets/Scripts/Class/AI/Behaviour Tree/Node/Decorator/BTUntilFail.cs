using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTUntilFail : BTDecorator
{
	public BTUntilFail(BTNode c) : base(c) {}
	
	public sealed override BTState Tick()
	{
		switch(child.Tick())
		{
			case BTState.Failure:
				return BTState.Success;

			case BTState.Success:
			case BTState.Running:
				return BTState.Running;

			default:
				return BTState.Failure;
		}
	}
}
