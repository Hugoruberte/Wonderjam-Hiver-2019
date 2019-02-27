using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;
using Tools;

public abstract class BTRandom : BTComposite
{
	protected Queue<int> remaining;

	public BTRandom(params BTNode[] cs) : base(cs)
	{
		this.InitializeRemaining(cs.Length);
	}

	protected int InitializeRemaining(int length)
	{
		List<int> l = new List<int>();
		for(int i = 0; i < length; i++) {
			l.Add(i);
		}
		l.Shuffle();

		remaining = new Queue<int>(l);
		
		return remaining.Dequeue();
	}
}
