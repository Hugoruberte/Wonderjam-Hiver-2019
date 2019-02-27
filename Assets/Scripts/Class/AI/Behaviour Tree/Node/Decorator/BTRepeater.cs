using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTRepeater : BTDecorator
{
	private int target;
	private int current;
	private BTState state;

	public BTRepeater(int target, BTNode c) : base(c)
	{
		this.target = target;
		this.current = -1;
	}

	public sealed override BTState Tick()
	{
		if(this.current == -1) {
			this.current = target;
		}

		state = child.Tick();

		if(state == BTState.Running)
		{
			return BTState.Running;
		}
		else
		{
			if(this.current > 0)
			{
				this.current --;
				return BTState.Running;
			}
			else
			{
				this.current = -1;
				return state;
			}
		}
	}
}
