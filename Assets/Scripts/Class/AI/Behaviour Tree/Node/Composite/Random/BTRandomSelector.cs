using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTRandomSelector : BTRandom
{
	public BTRandomSelector(params BTNode[] cs) : base(cs) {}
	
	public sealed override BTState Tick()
	{
		this.childState = children[activeChild].Tick();

		switch(this.childState)
		{
			case BTState.Failure:
				if(this.remaining.Count == 0){
					this.activeChild = this.InitializeRemaining(this.children.Count);
					return BTState.Failure;
				}
				else {
					this.bt.currentRunningNode = this;
					this.activeChild = this.remaining.Dequeue();
					return BTState.Running;
				}

			case BTState.Running:
				this.bt.currentRunningNode = this.children[this.activeChild];
				return BTState.Running;

			case BTState.Success:
				this.activeChild = this.InitializeRemaining(this.children.Count);
				return BTState.Success;

			default:
				return BTState.Failure;
		}
	}
}
