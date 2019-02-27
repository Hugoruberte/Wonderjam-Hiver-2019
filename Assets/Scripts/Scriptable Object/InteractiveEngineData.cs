using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactive.Engine
{
	[Serializable]
	public struct ChemicalToArrayData {
		public ChemicalElement element;
		public ChemicalElement[] array;

		public ChemicalToArrayData(ChemicalElement e, ChemicalElement[] a) {
			this.element = e;
			this.array = a;
		}
	}

	[Serializable]
	public struct IntToChemicalData {
		public int couple;
		public ChemicalElement type;
		public bool empty;

		public IntToChemicalData(int c, ChemicalElement t, bool e = false) {
			this.couple = c;
			this.type = t;
			this.empty = e;
		}
	}

	[CreateAssetMenu(fileName = "InteractiveEngineData", menuName = "Scriptable Object/Other/InteractiveEngineData", order = 3)]
	public class InteractiveEngineData : ScriptableObject
	{
		[HideInInspector] public List<ChemicalElementMixEntity> chemicalElementMixEntityPoolList = new List<ChemicalElementMixEntity>();
		[HideInInspector] public List<ChemicalElement> chemicalElementPoolList = new List<ChemicalElement>();

		[HideInInspector] public List<ChemicalToArrayData> primaries = new List<ChemicalToArrayData>();
		[HideInInspector] public List<ChemicalToArrayData> weaknesses = new List<ChemicalToArrayData>();
		[HideInInspector] public List<IntToChemicalData> couples = new List<IntToChemicalData>();
		[HideInInspector] public List<IntToChemicalData> winners = new List<IntToChemicalData>();

		private StringBuilder stringBuilder = new StringBuilder();
		private const string voiddString = "Interactive.Engine.Voidd";




		public bool HasPrimariesOf(ChemicalElementEntity ent)
		{
			// only does that
			return this.primaries.Exists(x => x.element == ent.type);
		}
		public void SetPrimariesOf(ChemicalElementEntity ent, ChemicalElement[] ps)
		{
			this.primaries.Add(new ChemicalToArrayData(ent.type, ps));
			this.primaries.Sort(CompareChemicalToArrayData);
		}
		public ChemicalElement[] GetPrimariesOf(ChemicalElementEntity ent)
		{
			ChemicalToArrayData data = this.primaries.Find(x => x.element == ent.type);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
				return null;
			}
		}



		public bool HasWeaknessesOf(ChemicalElementEntity ent)
		{
			// only does that
			return this.weaknesses.Exists(x => x.element == ent.type);
		}
		public void SetWeaknessesOf(ChemicalElementEntity ent, ChemicalElement[] ps)
		{
			this.weaknesses.Add(new ChemicalToArrayData(ent.type, ps));
			this.weaknesses.Sort(CompareChemicalToArrayData);
		}
		public ChemicalElement[] GetWeaknessesOf(ChemicalElementEntity ent)
		{
			ChemicalToArrayData data = this.weaknesses.Find(x => x.element == ent.type);
			if(data.array != null) {
				return data.array;
			} else {
				Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
				return null;
			}
		}


		public bool HasMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		{
			int couple = (int)(a.type | b.type);
			return this.couples.Exists(x => x.couple == couple);
		}
		public void SetMixOf(ChemicalElementEntity a, ChemicalElementEntity b, ChemicalElementEntity ent)
		{
			int couple = (int)(a.type | b.type);
			if(!this.couples.Exists(x => x.couple == couple)) {
				if(ent == null) {
					this.couples.Add(new IntToChemicalData(couple, ChemicalElement.Voidd, true));
				} else {
					this.couples.Add(new IntToChemicalData(couple, ent.type));
				}
			} else {
				Debug.LogWarning($"WARNING : This couple ({a} + {b}) is already registered ! Check it out for optimization !");
			}
		}
		public string GetMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		{
			int couple = (int)(a.type | b.type);
			IntToChemicalData data = this.couples.Find(x => x.couple == couple);
			if(data.couple > 0) {
				if(data.empty) {
					return null;
				}
				this.stringBuilder.Clear();
				this.stringBuilder.Append("Interactive.Engine.").Append(data.type.ToString());
				return this.stringBuilder.ToString();
			} else {
				Debug.LogWarning($"WARNING : This couple ({a} + {b}) is not yet registered ! Check it out !");
				return voiddString;
			}
		}


		public bool HasWinnerBetween(ChemicalElementEntity main, ChemicalElementEntity other)
		{
			int couple = (int)(main.type | other.type);
			return this.winners.Exists(x => x.couple == couple);
		}
		public void SetWinnerBetween(ChemicalElementEntity main, ChemicalElementEntity other, bool isMainWinning)
		{
			int couple = (int)(main.type | other.type);
			if(!this.winners.Exists(x => x.couple == couple)) {
				if(isMainWinning) {
					this.winners.Add(new IntToChemicalData(couple, main.type));
				} else {
					this.winners.Add(new IntToChemicalData(couple, other.type));
				}
			} else {
				Debug.LogWarning($"WARNING : This couple ({main} + {other}) is already registered ! Check it out for optimization !");
			}
		}
		public bool IsWinningAgainst(ChemicalElementEntity main, ChemicalElementEntity other)
		{
			int couple = (int)(main.type | other.type);
			IntToChemicalData data = this.winners.Find(x => x.couple == couple);
			if(data.couple > 0) {
				return (data.type == main.type);
			} else {
				Debug.LogWarning($"WARNING : This couple ({main} + {other}) is not yet registered ! Check it out !");
				return false;
			}
		}





		private static int CompareChemicalToArrayData(ChemicalToArrayData x, ChemicalToArrayData y)
		{
			int xe = (int)x.element;
			int ye = (int)y.element;

			if(xe > ye) {
				return 1;
			} else if(xe < ye) {
				return -1;
			} else {
				return 0;
			}
		}






















































		
		// public bool HasPrimariesOf(ChemicalElementEntity ent)
		// {
		// 	return this.primaries.ContainsKey(ent.type);
		// }
		// public void SetPrimariesOf(ChemicalElementEntity ent, ChemicalElement[] ps)
		// {
		// 	if(!this.primaries.ContainsKey(ent.type)) {
		// 		this.primaries.Add(ent.type, ps);
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This element ({ent.type}) is already registered ! Check it out for optimization !");
		// 	}
		// }
		// public ChemicalElement[] GetPrimariesOf(ChemicalElementEntity ent)
		// {
		// 	if(this.primaries.ContainsKey(ent.type)) {
		// 		return this.primaries[ent.type];
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
		// 		return null;
		// 	}
		// }



		// public bool HasWeaknessesOf(ChemicalElementEntity ent)
		// {
		// 	return this.weaknesses.ContainsKey(ent.type);
		// }
		// public void SetWeaknessesOf(ChemicalElementEntity ent, ChemicalElement[] ws)
		// {
		// 	if(!this.weaknesses.ContainsKey(ent.type)) {
		// 		this.weaknesses.Add(ent.type, ws);
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This element ({ent.type}) is already registered ! Check it out for optimization !");
		// 	}
		// }
		// public ChemicalElement[] GetWeaknessesOf(ChemicalElementEntity ent)
		// {
		// 	if(this.weaknesses.ContainsKey(ent.type)) {
		// 		return this.weaknesses[ent.type];
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This element ({ent.type}) is not yet registered ! Check it out !");
		// 		return null;
		// 	}
		// }


		// public bool HasMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		// {
		// 	int couple = (int)(a.type | b.type);
		// 	return this.couples.ContainsKey(couple);
		// }
		// public void SetMixOf(ChemicalElementEntity a, ChemicalElementEntity b, ChemicalElementEntity ent)
		// {
		// 	int couple = (int)(a.type | b.type);
		// 	if(!this.couples.ContainsKey(couple)) {
		// 		this.couples.Add(couple, ent);
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This couple ({a} + {b}) is already registered ! Check it out for optimization !");
		// 	}
		// }
		// public ChemicalElementEntity GetMixOf(ChemicalElementEntity a, ChemicalElementEntity b)
		// {
		// 	int couple = (int)(a.type | b.type);
		// 	if(this.couples.ContainsKey(couple)) {
		// 		return this.couples[couple].Spawn();
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This couple ({a} + {b}) is not yet registered ! Check it out !");
		// 		return null;
		// 	}
		// }


		// public bool HasWinnerBetween(ChemicalElementEntity main, ChemicalElementEntity other)
		// {
		// 	int couple = (int)(main.type | other.type);
		// 	return this.winners.ContainsKey(couple);
		// }
		// public void SetWinnerBetween(ChemicalElementEntity main, ChemicalElementEntity other, bool isMainWinning)
		// {
		// 	int couple = (int)(main.type | other.type);
		// 	if(!this.winners.ContainsKey(couple)) {
		// 		if(isMainWinning) {
		// 			this.winners.Add(couple, main);
		// 		} else {
		// 			this.winners.Add(couple, other);
		// 		}
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This couple ({main} + {other}) is already registered ! Check it out for optimization !");
		// 	}
		// }
		// public bool IsWinningAgainst(ChemicalElementEntity main, ChemicalElementEntity other)
		// {
		// 	int couple = (int)(main.type | other.type);
		// 	if(this.winners.ContainsKey(couple)) {
		// 		return (this.winners[couple].type == main.type);
		// 	} else {
		// 		Debug.LogWarning($"WARNING : This couple ({main} + {other}) is not yet registered ! Check it out !");
		// 		return false;
		// 	}
		// }
	}
}


