using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTRandomSequence : BTRandom
{
	public BTRandomSequence(params BTNode[] cs) : base(cs) {}
	
	public sealed override BTState Tick()
	{
		this.childState = children[activeChild].Tick();

		switch(this.childState)
		{
			case BTState.Success:
				if(this.remaining.Count == 0){
					this.activeChild = this.InitializeRemaining(this.children.Count);
					return BTState.Success;
				}
				else {
					this.bt.currentRunningNode = this;
					this.activeChild = this.remaining.Dequeue();
					return BTState.Running;
				}

			case BTState.Running:
				this.bt.currentRunningNode = this.children[this.activeChild];
				return BTState.Running;

			case BTState.Failure:
				this.activeChild = this.InitializeRemaining(this.children.Count);
				return BTState.Failure;

			default:
				return BTState.Failure;
		}
	}
}
