using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Interactive.Engine;

[CustomEditor(typeof(InteractiveEngineData))]
public class InteractiveEngineDataEditor : Editor
{
	private bool showPrimaries = true;
	private bool showAlcoholAttributes = true;
	private bool showAlcoholColors = true;

	private ChemicalElement[] array_e;
	private AlcoholAttribute[] array_a;
	private AlcoholColor[] array_c;

	public override void OnInspectorGUI()
	{
		InteractiveEngineData script = target as InteractiveEngineData;

		DrawDefaultInspector();

		showPrimaries = EditorGUILayout.Foldout(showPrimaries, "Primaries");
		if(showPrimaries) {
			EditorGUI.indentLevel += 2;
			foreach(ChemicalToArrayData e in script.primaries) {
				EditorGUILayout.LabelField(e.element.ToString(), EditorStyles.boldLabel);

				array_e = e.array;

				EditorGUI.indentLevel += 2;
				foreach(ChemicalElement k in array_e) {
					EditorGUILayout.LabelField(k.ToString(), EditorStyles.miniLabel);
				}
				EditorGUILayout.Space();
				EditorGUI.indentLevel -= 2;
			}
			EditorGUI.indentLevel -= 2;
		}
		
		showAlcoholAttributes = EditorGUILayout.Foldout(showAlcoholAttributes, "Attributes");
		if(showAlcoholAttributes) {
			EditorGUI.indentLevel += 2;
			foreach(ChemicalToAlcoholAttributesData e in script.attributes) {
				EditorGUILayout.LabelField(e.element.ToString(), EditorStyles.boldLabel);

				array_a = e.array;

				if(array_a != null) {
					EditorGUI.indentLevel++;
					foreach(AlcoholAttribute k in array_a) {
						EditorGUILayout.LabelField(k.ToString(), EditorStyles.miniLabel);
					}
					EditorGUI.indentLevel--;
				}
				
				EditorGUILayout.Space();
			}
			EditorGUI.indentLevel -= 2;
		}

		showAlcoholColors = EditorGUILayout.Foldout(showAlcoholColors, "Colors");
		if(showAlcoholColors) {
			EditorGUI.indentLevel += 2;
			foreach(ChemicalToColorData e in script.colors) {
				EditorGUILayout.LabelField(e.element.ToString(), EditorStyles.boldLabel);

				array_c = e.array;

				EditorGUI.indentLevel++;
				foreach(AlcoholColor k in array_c) {
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
