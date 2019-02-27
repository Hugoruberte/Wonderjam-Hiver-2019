using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTSelector : BTComposite
{
	public BTSelector(params BTNode[] cs) : base(cs){}
	
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
			case BTState.Failure:
				this.activeChild ++;
				if(this.activeChild == this.children.Count){
					this.activeChild = 0;
					return BTState.Failure;
				}
				else {
					this.bt.currentRunningNode = this;
					return BTState.Running;
				}

			case BTState.Running:
				this.bt.currentRunningNode = this.children[this.activeChild];
				return BTState.Running;

			case BTState.Success:
				this.activeChild = 0;
				return BTState.Success;

			default:
				return BTState.Failure;
		}
	}
}
