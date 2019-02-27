using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public abstract class BTBlock : BTNode
{
	public BTNode Sequence(params BTNode[] cs)
	{
		return this.InitializeActioner(bt.Sequence(cs));
	}

	public BTNode Selector(params BTNode[] cs)
	{
		return this.InitializeActioner(bt.Selector(cs));
	}

	private BTNode InitializeActioner(BTComposite act)
	{
		act.condition = this;
		return act;
	}
}
