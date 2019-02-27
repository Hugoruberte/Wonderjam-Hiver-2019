using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Object/Main/GameData", order = 3)]
public class GameData : ScriptableObject
{
	[HideInInspector]
	public Transform garbage;
}
