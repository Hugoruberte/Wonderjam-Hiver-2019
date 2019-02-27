using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTTest : BTLeaf
{
	protected System.Func<bool> fn;

	public BTTest(System.Func<bool> fn)
	{
		this.fn = fn;
	}

	public sealed override BTState Tick()
	{
		return this.fn() ? BTState.Success : BTState.Failure;
	}

	public override string ToString()
	{
		return "Test : " + fn.Method.ToString();
	}
}