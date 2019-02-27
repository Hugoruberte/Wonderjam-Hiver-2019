using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.AI
{
	[System.Serializable]
	public class UtilityAI
	{
		private List<UtilityAction> selected = new List<UtilityAction>();

		public UtilityAction Select(MovementController ctr, List<UtilityAction> actions)
		{
			UtilityAction result;
			int max;
			int score;
			int nb;

			result = null;
			max = int.MinValue;
			nb = 0;

			this.selected.Clear();

			foreach(UtilityAction act in actions) {
				// discard this action, it is not active
				if(!act.active) {
					continue;
				}

				// discard this action, best it can do won't be greater than the current max
				if(act.Max() < max) {
					continue;
				}

				score = act.Score(ctr);
				
				if(score > max) {
					max = score;
					result = act;
					this.selected.Clear();
					nb = 1;
				} else if(score == max) {
					this.selected.Add(act);
					nb ++;
				}
			}

			if(nb > 1) {
				result = this.selected[UnityEngine.Random.Range(0, nb)];
			} else if(nb == 0) {
				Debug.LogWarning("WARNING : This Utility AI does not have any action.");
				result = null;
			}

			return result;
		}
	}
}

