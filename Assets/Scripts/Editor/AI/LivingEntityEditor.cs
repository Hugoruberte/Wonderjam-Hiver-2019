using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LivingEntity), true), CanEditMultipleObjects]
public class LivingEntityEditor : InteractiveEntityEditor
{
	private string[] guids;
	private string path;
	private string nameAI;

	private bool showAI = false;
	private UtilityAction act;

	// float val = 0f;

	void OnEnable()
	{
		LivingEntity script = target as LivingEntity;

		nameAI = script.GetType().ToString();
		nameAI = nameAI.Replace(" ", string.Empty);
		nameAI += "AI";
	}


	public override void OnInspectorGUI()
	{
		// Rect n = new Rect();
		// n.x = rect.x;
		// n.y += 500;
		// n.width = 200;
		// n.height = 16;
		// val = EditorGUI.FloatField(n, "Val", val);

		LivingEntity script = target as LivingEntity;

		base.OnInspectorGUI();

		if(script.behaviour == null) {
			guids = AssetDatabase.FindAssets("t:" + nameAI);
			if(guids.Length > 0) {
				path = AssetDatabase.GUIDToAssetPath(guids[0]);
				script.behaviour = AssetDatabase.LoadAssetAtPath(path, typeof(UtilityAIBehaviour)) as UtilityAIBehaviour;
			}
		}

		rect = EditorGUILayout.GetControlRect(true, 0);
		rect.y += 1;
		rect.width += -120;
		rect.height = 16;

		EditorGUI.LabelField(rect, script.GetType() + " AI", EditorStyles.boldLabel);

		rect.x += 120;
		rect.height = 17;

		GUI.enabled = false;
		EditorGUI.ObjectField(rect, MonoScript.FromScriptableObject(script.behaviour), typeof(UtilityAIBehaviour), false);
		GUI.enabled = true;

		GUILayout.Space(19);

		rect.x += -120;
		rect.y += -0.5f;
		showAI = EditorGUI.Foldout(rect, showAI, "");

		if(showAI) {
			EditorGUI.indentLevel ++;
			
			if(EditorApplication.isPlaying) {
				act = script.behaviour.GetCurrentAction(script);
				EditorGUILayout.LabelField($"Current action : {act.method} (score = {act.score})", EditorStyles.centeredGreyMiniLabel);
			} else {
				EditorGUILayout.LabelField($"Current action : None (score = 0)", EditorStyles.centeredGreyMiniLabel);
			}
			
			EditorGUI.indentLevel --;
			GUILayout.Space(5);
		}

		serializedObject.ApplyModifiedProperties();
	}
}

