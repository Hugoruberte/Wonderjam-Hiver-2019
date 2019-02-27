using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTParallel : BTComposite
{
	private List<int> remaining;
	private int numberOfSuccess;
	private int index;

	public BTParallel(params BTNode[] cs) : base(cs)
	{
		this.remaining = new List<int>();
		for(int i = 0; i < cs.Length; i++) {
			this.remaining.Add(i);
		}

		this.numberOfSuccess = 0;
	}

	public sealed override BTState Tick(BTState overrideState)
	{
		for(int i = this.children.Count - 1; i >= 0; i--) {

			this.index = this.remaining[i];
			this.childState = this.children[this.index].Tick();

			switch(this.childState)
			{
				case BTState.Success:
					this.numberOfSuccess ++;
					this.remaining.Remove(this.index);
					if(this.remaining.Count == 0) {
						if(this.numberOfSuccess == this.children.Count) {
							return BTState.Success;
						} else {
							return BTState.Failure;
						}
					}
					break;

				case BTState.Failure:
					this.remaining.Remove(this.index);
					if(this.remaining.Count == 0) {
						if(this.numberOfSuccess == this.children.Count) {
							return BTState.Success;
						} else {
							return BTState.Failure;
						}
					}
					break;

				case BTState.Running:
					break;

				default:
					return BTState.Failure;
			}
		}

		this.bt.currentRunningNode = this;
		return BTState.Running;
	}
}