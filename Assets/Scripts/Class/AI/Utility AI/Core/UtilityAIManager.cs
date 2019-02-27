using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIManager : Singleton<UtilityAIManager>
{
	public UtilityAIBehaviour[] behaviours;

	protected override void Awake()
	{
		base.Awake();
		
		foreach(UtilityAIBehaviour b in this.behaviours) {
			b.OnAwake();
		}
	}

	protected override void Start()
	{
		foreach(UtilityAIBehaviour b in this.behaviours) {
			b.OnStart();
		}
	}

	void Update()
	{
		foreach(UtilityAIBehaviour b in this.behaviours) {

			if(Time.time - b.lastUpdate < b.updateRate) {
				continue;
			}

			b.lastUpdate = Time.time;

			b.UpdateUtilityActions(this);
		}
	}
}
