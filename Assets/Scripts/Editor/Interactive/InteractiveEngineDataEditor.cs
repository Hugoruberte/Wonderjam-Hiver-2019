using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Interactive.Engine;

[CustomEditor(typeof(InteractiveEngineData))]
public class InteractiveEngineDataEditor : Editor
{
	private bool showPrimaries = true;
	private bool showWeaknesses = true;

	private ChemicalElement[] array;

	public override void OnInspectorGUI()
	{
		InteractiveEngineData script = target as InteractiveEngineData;

		DrawDefaultInspector();

		showPrimaries = EditorGUILayout.Foldout(showPrimaries, "Primaries");
		if(showPrimaries) {
			EditorGUI.indentLevel += 2;
			foreach(ChemicalToArrayData e in script.primaries) {
				EditorGUILayout.LabelField(e.element.ToString(), EditorStyles.boldLabel);

				array = e.array;

				EditorGUI.indentLevel += 2;
				foreach(ChemicalElement k in array) {
					EditorGUILayout.LabelField(k.ToString(), EditorStyles.miniLabel);
				}
				EditorGUILayout.Space();
				EditorGUI.indentLevel -= 2;
			}
			EditorGUI.indentLevel -= 2;
		}
		
		showWeaknesses = EditorGUILayout.Foldout(showWeaknesses, "Weaknesses");
		if(showWeaknesses) {
			EditorGUI.indentLevel += 2;
			foreach(ChemicalToArrayData e in script.weaknesses) {
				EditorGUILayout.LabelField(e.element.ToString(), EditorStyles.boldLabel);

				array = e.array;

				EditorGUI.indentLevel++;
				foreach(ChemicalElement k in array) {
					EditorGUILayout.LabelField(k.ToString(), EditorStyles.miniLabel);
				}
				EditorGUILayout.Space();
				EditorGUI.indentLevel--;
			}
			EditorGUI.indentLevel -= 2;
		}
		

		/*foreach(int c in script.couples.Keys) {
			ChemicalElementEntity e = script.couples[c];
			// Show element
		}

		foreach(int c in script.winners.Keys) {
			ChemicalElement e = script.winners[c];
			// Show element
		}*/
	}
}
