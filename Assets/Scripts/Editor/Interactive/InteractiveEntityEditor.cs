using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Interactive.Engine;

[CustomEditor(typeof(InteractiveEntity), true), CanEditMultipleObjects]
public class InteractiveEntityEditor : Editor
{
	private bool showInteractiveInfo = false;
	private bool hasCell;
	
	protected Rect rect;

	// float val = 0f;

	public override void OnInspectorGUI()
	{
		InteractiveEntity script = target as InteractiveEntity;

		DrawDefaultInspector();

		showInteractiveInfo = EditorGUILayout.Foldout(showInteractiveInfo, "");
		rect = EditorGUILayout.GetControlRect(true, 0);
		rect.y += -18;
		rect.height += 20;
		EditorGUI.LabelField(rect, "Interactive Information", EditorStyles.boldLabel);

		/*Rect n = new Rect();
		n.x = rect.x;
		n.y += 500;
		n.width = 200;
		n.height = 16;
		val = EditorGUI.FloatField(n, "Val", val);*/

		if(showInteractiveInfo) {
			EditorGUI.indentLevel++;
			GUI.enabled = false;

			rect.x += rect.width + -110;

			if(EditorApplication.isPlaying) {
				// physical state
				rect.y += 55;
				EditorGUILayout.LabelField("Physical state");
				EditorGUI.LabelField(rect, script.physical.ToString(), EditorStyles.boldLabel);

				// chemical element
				rect.y += -18;
				EditorGUILayout.LabelField("Chemical element");
				EditorGUI.LabelField(rect, script.chemical.ToString(), EditorStyles.boldLabel);

				// chemical material
				rect.y += -18;
				EditorGUILayout.LabelField("Chemical material");
				EditorGUI.LabelField(rect, script.material.ToString(), EditorStyles.boldLabel);
			}

			// life
			GUI.enabled = true;
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField($"Life : {script.life}", EditorStyles.centeredGreyMiniLabel);

			EditorGUILayout.Space();
		}
		
		serializedObject.ApplyModifiedProperties();
	}
}

