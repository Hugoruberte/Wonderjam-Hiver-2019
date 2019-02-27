using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class BTDo : BTLeaf
{
	private System.Action fn;

	private System.Func<IEnumerator<BTState>> coroutineFactory;
	private IEnumerator<BTState> coroutine;

	private BTState result;

	private float tick;


	public BTDo(System.Action f)
	{
		this.fn = f;
	}

	public BTDo(System.Func<IEnumerator<BTState>> coroutineFactory)
	{
		this.coroutineFactory = coroutineFactory;
		this.tick = 0f;
	}

	public sealed override BTState Tick()
	{
		if(fn != null)
		{
			fn();
			return BTState.Success;
		}
		else
		{
			if(coroutine == null || this.bt.previousTick != this.tick) {
				coroutine = coroutineFactory();
			}

			this.tick = this.bt.currentTick;

			if(!coroutine.MoveNext()){
				coroutine = null;
				return BTState.Success;
			}

			result = coroutine.Current;

			if(result == BTState.Running) {
				return BTState.Running;
			}
			else {
				coroutine = null;
				return result;
			}
		}
	}
}