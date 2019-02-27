using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class UtilityAction
{
	// scorers
	public List<UtilityScorer> scorers = new List<UtilityScorer>();

	// inspector
	public int index = 0;
	public int score = 0;
	public string method = "";
	public bool active = true;

	// invocation
	public System.Action<MovementController> action = null;
	public System.Func<MovementController, UtilityAction, IEnumerator> coroutineFactory = null;
	private IEnumerator coroutine = null;

	// state
	[System.NonSerialized] public bool isStoppable = false; // to be changed by the coroutine launched by this UtilityAction !
	[System.NonSerialized] public bool isRunning = false; // to be changed by the coroutine launched by this UtilityAction !
	



	public UtilityAction(string method, int index)
	{
		this.index = index;
		this.method = method;
	}

	public int Score(MovementController ctr)
	{
		score = 0;
		foreach(UtilityScorer s in this.scorers) {
			score += s.Score(ctr);
		}

		return score;
	}

	public int Max()
	{
		int max;

		max = 0;
		foreach(UtilityScorer s in this.scorers) {
			max += s.Max();
		}

		return max;
	}

	public void Start(MovementController ctr, UtilityAIManager main)
	{
		if(this.action != null) {
			this.isRunning = false;
			this.action(ctr);
		} else {
			this.coroutine = this.coroutineFactory(ctr, this);
			this.isRunning = true;
			main.StartCoroutine(this.coroutine);
		}
	}

	public void Stop(UtilityAIManager main)
	{
		if(this.action != null) {
			Debug.LogError("ERROR: Could not stop this UtilityAction because it is not a coroutine !");
			return;
		}

		main.StopCoroutine(this.coroutine);
	}







	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* -------------------------------------- UTIL FUNCTION ---------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	public void Initialize(UtilityAIBehaviour t)
	{
		MethodInfo methodInfo;

		this.isStoppable = false;
		this.isRunning = false;

		methodInfo = t.GetType().GetMethod(method);

		if(methodInfo.ReturnType == typeof(void)) {
			this.action = System.Action<MovementController>.CreateDelegate(typeof(System.Action<MovementController>), t, methodInfo) as System.Action<MovementController>;
		} else {
			this.coroutineFactory = System.Func<MovementController, UtilityAction, IEnumerator>.CreateDelegate(typeof(System.Func<MovementController, UtilityAction, IEnumerator>), t, methodInfo) as System.Func<MovementController, UtilityAction, IEnumerator>;
		}

		foreach(UtilityScorer sc in this.scorers) {
			sc.Initialize(t);
		}
	}








	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* ----------------------------------- INSPECTOR FUNCTIONS ------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	/* --------------------------------------------------------------------------------------------*/
	public void AddCondition(string method)
	{
		UtilityScorer scorer = new UtilityScorer(true, method);
		this.scorers.Add(scorer);
	}

	public void AddCurve(string method)
	{
		UtilityScorer scorer = new UtilityScorer(false, method);
		this.scorers.Add(scorer);
	}

	public void RemoveScorerAt(int index)
	{
		this.scorers.RemoveAt(index);
	}
}