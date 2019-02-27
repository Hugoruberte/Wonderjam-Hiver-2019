using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeAI
{
	public enum BTState
	{
		Success = 0,
		Failure,
		Running,
		None
	}

	public abstract class BehaviourTree
	{
		private BTNode _root;
		protected BTNode root {
			set {
				this._root = value;
				if(this._root != null)
					this._root.bt = this;
			}
			get {
				return this._root;
			}
		}

		private BTState currentRunningNodeState;
		private BTNode _currentRunningNode;
		public BTNode currentRunningNode {
			set {
				this._currentRunningNode = (value == this._root) ? null : value;
			}
			get {
				return this._currentRunningNode;
			}
		}

		private float _previousTick = 0f;
		public float previousTick {
			get { return this._previousTick; }
		}
		private float _currentTick = 0f;
		public float currentTick {
			get { return this._currentTick; }
		}


		public BehaviourTree()
		{
			this.root = null;
			this.currentRunningNode = null;
		}

		public void Tick()
		{
			this._previousTick = this._currentTick;
			this._currentTick = Time.time;

			/*if(this.currentRunningNode == null)
			{
				this.root.Tick();
			}
			else
			{
				this.currentRunningNodeState = this.currentRunningNode.Tick();
				if(this.currentRunningNodeState != BTState.Running)
				{
					this.currentRunningNode.parent.Tick(this.currentRunningNodeState);
					this.currentRunningNode = null;
				}
			}*/

			this.root.Tick();
		}





		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* ------------------------------------- COMPOSITE NODES --------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		public BTSequence Sequence(params BTNode[] cs)
		{
			BTSequence node = new BTSequence(cs);
			node.bt = this;
			return node;
		}

		public BTSelector Selector(params BTNode[] cs)
		{
			BTSelector node = new BTSelector(cs);
			node.bt = this;
			return node;
		}

		public BTRandomSelector RandomSelector(params BTNode[] cs)
		{
			BTRandomSelector node = new BTRandomSelector(cs);
			node.bt = this;
			return node;
		}

		public BTRandomSequence RandomSequence(params BTNode[] cs)
		{
			BTRandomSequence node = new BTRandomSequence(cs);
			node.bt = this;
			return node;
		}

		public BTParallel Parallel(params BTNode[] cs)
		{
			BTParallel node = new BTParallel(cs);
			node.bt = this;
			return node;
		}




		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* ------------------------------------- DECORATOR NODES --------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		public BTInverter Not(BTNode c)
		{
			BTInverter node = new BTInverter(c);
			node.bt = this;
			return node;
		}

		public BTSucceeder Succeeder(BTNode c)
		{
			BTSucceeder node = new BTSucceeder(c);
			node.bt = this;
			return node;
		}

		public BTRepeater Repeat(int target, BTNode c)
		{
			BTRepeater node = new BTRepeater(target, c);
			node.bt = this;
			return node;
		}

		public BTUntilFail RepeatUntilFail(BTNode c)
		{
			BTUntilFail node = new BTUntilFail(c);
			node.bt = this;
			return node;
		}





		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------- BLOCK NODES ----------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		public BTIf If(System.Func<bool> fn)
		{
			BTIf node = new BTIf(fn);
			node.bt = this;
			return node;
		}






		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* ---------------------------------------- LEAF NODE -----------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		/* --------------------------------------------------------------------------------------------*/
		public BTDo Do(System.Action fn)
		{
			BTDo node = new BTDo(fn);
			node.bt = this;
			return node;
		}

		public BTDo Do(System.Func<IEnumerator<BTState>> co)
		{
			BTDo node = new BTDo(co);
			node.bt = this;
			return node;
		}

		public BTTest Test(System.Func<bool> fn)
		{
			BTTest node = new BTTest(fn);
			node.bt = this;
			return node;
		}

		public BTWait Wait(float duration)
		{
			BTWait node = new BTWait(duration);
			node.bt = this;
			return node;
		}
	}
}

