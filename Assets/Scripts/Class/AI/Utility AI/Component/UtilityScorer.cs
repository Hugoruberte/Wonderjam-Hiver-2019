using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I am forced to use an 'isCondition' variable instead of create
// two subclass like it was done below because of Unity serialization
// which do not manage polymorphism and inheritance...
// I am as sad as you.

[System.Serializable]
public class UtilityScorer
{
	public bool isCondition = true;
	public bool not = false;

	// score
	public int score = 0;
	public AnimationCurve curve = null;
	[SerializeField] private int _max = int.MaxValue;

	// inspector
	public string method;
	public int index = 0;

	// action
	private System.Func<MovementController, bool> condition;
	private System.Func<MovementController, float> mapper;


	public UtilityScorer(bool c, string method)
	{
		this.isCondition = c;
		this.method = method;
	}

	public int Score(MovementController ctr)
	{
		int res;
		bool cond;

		if(this.isCondition) {
			cond = this.condition(ctr);
			res = ((!cond && this.not) || (cond && !this.not)) ? score : 0;
		} else {
			res = Mathf.RoundToInt(this.curve.Evaluate(this.mapper(ctr)));
		}

		return res;
	}

	public int Max()
	{
		if(this._max < int.MaxValue) {
			return this._max;
		}

		int max;

		max = int.MinValue;

		if(isCondition) {
			max = score;
		} else {
			int val;
			for(float step = 0f; step <= 1f; step += 0.1f) {
				val = Mathf.RoundToInt(this.curve.Evaluate(step));
				if(val > max) {
					max = val;
				}
			}
		}

		this._max = max;
		return max;
	}

	public void Initialize(UtilityAIBehaviour target)
	{
		if(this.isCondition) {
			this.condition = System.Func<MovementController, bool>.CreateDelegate(typeof(System.Func<MovementController, bool>), target, target.GetType().GetMethod(method)) as System.Func<MovementController, bool>;
		} else {
			this.mapper = System.Func<MovementController, float>.CreateDelegate(typeof(System.Func<MovementController, float>), target, target.GetType().GetMethod(method)) as System.Func<MovementController, float>;
		}
	}
}