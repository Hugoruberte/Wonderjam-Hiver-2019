using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTDecorator : BTNode
{
	protected BTNode child;

	public BTDecorator(BTNode c)
	{
		this.child = c;
		this.child.parent = this;
	}
}
