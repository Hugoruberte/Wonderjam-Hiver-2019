using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public abstract class BTComposite : BTNode
{
	protected List<BTNode> children;

	protected BTState childState;
	protected int activeChild;

	public BTBlock condition = null;

	public BTComposite(params BTNode[] cs)
	{
		this.children = new List<BTNode>();

		for(int i = 0; i < cs.Length; i++) {
			cs[i].parent = this;
			this.children.Add(cs[i]);
		}

		this.activeChild = 0;
	}

	protected BTState CheckCondition()
	{
		return (this.condition == null) ? BTState.Success : this.condition.Tick();
	}
}
