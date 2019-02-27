using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTSequence : BTComposite
{
	public BTSequence(params BTNode[] cs) : base(cs) {}

	public sealed override BTState Tick(BTState overrideState)
	{
		switch(this.CheckCondition())
		{
			case BTState.Success:
				break;
			case BTState.Failure:
				return BTState.Failure;
			case BTState.Running:
				return BTState.Running;
		}

		this.childState = (overrideState == BTState.None) ? this.children[this.activeChild].Tick() : overrideState;

		switch(this.childState)
		{
			case BTState.Success:
				this.activeChild ++;
				if(this.activeChild == this.children.Count){
					this.activeChild = 0;
					return BTState.Success;
				}
				else {
					this.bt.currentRunningNode = this;
					return BTState.Running;
				}

			case BTState.Running:
				this.bt.currentRunningNode = this.children[this.activeChild];
				return BTState.Running;

			case BTState.Failure:
				this.activeChild = 0;
				return BTState.Failure;

			default:
				return BTState.Failure;
		}
	}
}
